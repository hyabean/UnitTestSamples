using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using MsgPack.Serialization;
using NUnit.Framework;
using Shouldly;

namespace MsgPack.Demo
{
    public class Program
    {
        static void Main(string[] args)
        {
            var memoryStream = new System.IO.MemoryStream();

            // Creates serializer.
            var serializer = MessagePackSerializer.Get<MyDtoClass>();
            
            MyDtoClass obj = new MyDtoClass();

            obj.ID = 2;
            obj.Name = "Test";
            obj.Type = MaleType.Female;
            
            // Pack obj to stream.
            serializer.Pack(memoryStream, obj);
            memoryStream.Position = 0;
            
            var length = memoryStream.Length;

            byte[] buffer = new byte[length];
            memoryStream.Read(buffer, 0, (int)length);

            memoryStream.Position = 0;
            // Unpack from stream.
            var unpackedObject = serializer.Unpack(memoryStream);

            //obj.ShouldSatisfyAllConditions(unpackedObject);
            obj.ID.ShouldBe(unpackedObject.ID);
            obj.Type.ShouldBe(unpackedObject.Type);
            obj.Name.ShouldBe(unpackedObject.Name);



            // Creates serializer.
            var serializer1 = MessagePackSerializer.Get<MyDtoStruct>();

            var memoryStream1 = new System.IO.MemoryStream();

            MyDtoStruct obj1 = new MyDtoStruct();

            obj1.ID = 2;
            obj1.Name = "Test";
            obj1.Type = MaleType.Female;

            // Pack obj to stream.
            serializer1.Pack(memoryStream1, obj1);
            memoryStream1.Position = 0;


            var length1 = memoryStream.Length;

            byte[] buffer1 = new byte[length1];
            memoryStream1.Read(buffer1, 0, (int)length1);

            memoryStream1.Position = 0;

            // Unpack from stream.
            var unpackedStruct1 = serializer1.Unpack(memoryStream1);

            Assert.AreEqual(obj1, unpackedStruct1);


            Assert.AreEqual(buffer, buffer1);

        }
    }


    public struct MyDtoStruct
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public MaleType Type { get; set; }

    }

    public class MyDtoClass
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public MaleType Type { get; set; }
        
    }

    public enum MaleType
    {
        Male,

        Female
    }
}
