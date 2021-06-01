namespace Domain.Seedwork.DataTypes
{
    using System;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [Serializable]
    public struct Range<T> : IEquatable<Range<T>>, IXmlSerializable where T : IComparable
    {
        public Range(T from, T to): this()
        {
            if (from?.CompareTo(to) > 0)
            {
                throw new ArgumentOutOfRangeException("'From' cannot be greater than 'To'", (Exception)null);
            }
            
            this.From = from;
            this.To = to;
        }

        public T From { get; private set; }

        public T To { get; private set; }

        public ValueComparison Compare(T value)
        {
            ValueComparison result = 0;
            var fromComparison = value.CompareTo(this.From);
            var toComparison = value.CompareTo(this.To);

            if (fromComparison < 0)
            {
                result |= ValueComparison.LessThanFrom;
            }                
            else if (fromComparison == 0)
            {
                result |= ValueComparison.EqualFrom;
            }                
            if (toComparison > 0)
            {
                result |= ValueComparison.GreaterThanTo;
            }                
            else if (toComparison == 0)
            {
                result |= ValueComparison.EqualTo;
            }
            
            if (result == 0)
            {
                result = ValueComparison.BetweenFromAndTo;
            }                
            return result;
        }

        public RangeComparison Compare(Range<T> value)
        {
            var fromfromComparison = value.From.CompareTo(this.From);
            var fromtoComparison = value.To.CompareTo(this.From);
            var tofromComparison = value.From.CompareTo(this.To);
            var totoComparison = value.To.CompareTo(this.To);

            if (fromfromComparison == 0 && totoComparison == 0)
            {
                return RangeComparison.Equals;
            }
            else if (fromfromComparison >= 0 && fromtoComparison > 0 && tofromComparison < 0 && totoComparison <= 0)
            {
                return RangeComparison.Range1IncludeRange2;
            }
            else if (fromfromComparison <= 0 && fromtoComparison > 0 && tofromComparison < 0 && totoComparison >= 0)
            {
                return RangeComparison.Range2IncludeRange1;
            }
            else if (fromfromComparison > 0 && fromtoComparison > 0 && tofromComparison < 0 && totoComparison > 0)
            {
                return RangeComparison.Range1InterceptsRange2;
            }
            else if (fromfromComparison < 0 && fromtoComparison > 0 && tofromComparison < 0 && totoComparison < 0)
            {
                return RangeComparison.Range2InterceptsRange1;
            }
            else if (fromfromComparison > 0 && fromtoComparison > 0 && tofromComparison >= 0 && totoComparison > 0)
            {
                return RangeComparison.Range1BeforeRange2;
            }
            else if (fromfromComparison < 0 && fromtoComparison <= 0 && tofromComparison < 0 && totoComparison < 0)
            {
                return RangeComparison.Range2BeforeRange1;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public bool Equals(Range<T> obj)
        {
            return object.Equals(this.From, obj.From) && object.Equals(this.To, obj.To);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }
            else if (obj is Range<T> otherRange)
            {
                return object.Equals(this.From, otherRange.From) && object.Equals(this.To, otherRange.To);
            }
            else
            {
                return base.Equals(obj);
            }
        }

        public override int GetHashCode()
        {
            return this.From.GetHashCode() ^ this.To.GetHashCode();
        }

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            T from = default(T);
            T to = default(T);
            reader.ReadStartElement();
            if (reader.Name == "From")
            {
                if (!reader.IsEmptyElement)
                {
                    var innerSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T), new XmlRootAttribute("From"));
                    using (var inner = reader.ReadSubtree())
                    {
                        from = (T)innerSerializer.Deserialize(inner);
                    }
                }
                reader.Skip();
            }
            if (reader.Name == "To")
            {
                if (!reader.IsEmptyElement)
                {
                    var innerSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T), new XmlRootAttribute("To"));
                    using (var inner = reader.ReadSubtree())
                    {
                        to = (T)innerSerializer.Deserialize(inner);
                    }
                }
                reader.Skip();
            }
            reader.ReadEndElement();

            var result = new Range<T>(from, to);
            this.From = result.From;
            this.To = result.To;
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            if (!(object.ReferenceEquals(this.From, null)))
            {
                var innerSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T), new XmlRootAttribute("From"));
                innerSerializer.Serialize(writer, this.From);
            }
            else
            {
                writer.WriteStartElement("From");
                writer.WriteEndElement();
            }
            if (!(object.ReferenceEquals(this.To, null)))
            {
                var innerSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T), new XmlRootAttribute("To"));
                innerSerializer.Serialize(writer, this.To);
            }
            else
            {
                writer.WriteStartElement("To");
                writer.WriteEndElement();
            }
        }

        public static bool operator ==(Range<T> obj1, Range<T> obj2)
        {
            return obj1.Equals(obj2);
        }

        public static bool operator !=(Range<T> obj1, Range<T> obj2)
        {
            return !obj1.Equals(obj2);
        }
    }

    public enum ValueComparison
    {
        LessThanFrom = 0b00001,
        EqualFrom = 0b00010,
        BetweenFromAndTo = 0b00100,
        EqualTo = 0b01000,
        Equals = 0b01010,
        GreaterThanTo = 0b10000
    }

    public enum RangeComparison
    {
        Range1BeforeRange2 = 1,
        Range1InterceptsRange2 = 2,
        Range1IncludeRange2 = 3,
        Equals = 4,
        Range2IncludeRange1 = 5,
        Range2InterceptsRange1 = 6,
        Range2BeforeRange1 = 7
    }
}
