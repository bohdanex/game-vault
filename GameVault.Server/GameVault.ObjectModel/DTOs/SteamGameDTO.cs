using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GameVault.ObjectModel.DTOs
{
    public class SteamGameDTO
    {
        public int SteamAppId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURI { get; set; }
        public double SteamPrice { get; set; }
        public double OurPrice { get; set; }
        public string Currency { get; set; }

        public class JsonConverter : JsonConverter<SteamGameDTO>
        {
            public override SteamGameDTO Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                SteamGameDTO steamGameDTO = new();

                reader.Read();
                reader.Read();
                reader.Read();
                reader.Read();

                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.PropertyName)
                    {
                        string propName = reader.GetString();
                        reader.Read();
                        if (propName is "name") steamGameDTO.Name = reader.GetString();
                        else if(propName is "detailed_description") steamGameDTO.Description = reader.GetString();
                        else if(propName is "header_image") steamGameDTO.ImageURI = reader.GetString();
                        else if(propName is "price_overview")
                        {
                            while (reader.Read())
                            {
                                if(reader.TokenType == JsonTokenType.PropertyName && reader.GetString() is "final")
                                {
                                    reader.Read();
                                    steamGameDTO.SteamPrice = reader.GetDouble();

                                    while (reader.Read());
                                }
                            }
                        }
                    }
                }

                return steamGameDTO;
            }

            public override void Write(Utf8JsonWriter writer, SteamGameDTO value, JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }
        }
    }
}
