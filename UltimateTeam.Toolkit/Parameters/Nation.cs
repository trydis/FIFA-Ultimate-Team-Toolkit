﻿namespace UltimateTeam.Toolkit.Parameters
{
    public class Nation : SearchParameterBase<uint>
    {
        public const uint Albania = 1;
        public const uint Algeria = 97;
        public const uint Angola = 98;
        public const uint AntiguaBarbuda = 63;
        public const uint Argentina = 52;
        public const uint Armenia = 3;
        public const uint Australia = 195;
        public const uint Austria = 4;
        public const uint Azerbaijan = 5;
        public const uint Bahrain = 150;
        public const uint Barbados = 66;
        public const uint Belarus = 6;
        public const uint Belgium = 7;
        public const uint Benin = 99;
        public const uint Bermuda = 68;
        public const uint Bolivia = 53;
        public const uint BosniaHerzegovina = 8;
        public const uint Brazil = 54;
        public const uint Bulgaria = 9;
        public const uint BurkinaFaso = 101;
        public const uint Burundi = 102;
        public const uint Cambodia = 154;
        public const uint Cameroon = 103;
        public const uint Canada = 70;
        public const uint CapeVerdeIslands = 104;
        public const uint CentralAfricanRep = 105;
        public const uint Chile = 55;
        public const uint ChinaPr = 155;
        public const uint Colombia = 56;
        public const uint Comoros = 214;
        public const uint Congo = 107;
        public const uint CostaRica = 72;
        public const uint Croatia = 10;
        public const uint Cuba = 73;
        public const uint Curacao = 85;
        public const uint Cyprus = 11;
        public const uint CzechRepublic = 12;
        public const uint Denmark = 13;
        public const uint DrCongo = 110;
        public const uint Ecuador = 57;
        public const uint Egypt = 111;
        public const uint ElSalvador = 76;
        public const uint England = 14;
        public const uint Estonia = 208;
        public const uint EquatorialGuinea = 112;
        public const uint FaroeIslands = 16;
        public const uint Fiji = 197;
        public const uint Finland = 17;
        public const uint France = 18;
        public const uint FrenchGuiana = 79;
        public const uint FyrMacedonia = 19;
        public const uint Gabon = 115;
        public const uint Gambia = 116;
        public const uint Georgia = 20;
        public const uint Germany = 21;
        public const uint Ghana = 117;
        public const uint Greece = 22;
        public const uint Grenada = 77;
        public const uint Guam = 157;
        public const uint Guatemala = 78;
        public const uint Guinea = 118;
        public const uint GuineaBissau = 119;
        public const uint Haiti = 80;
        public const uint Honduras = 81;
        public const uint Hungary = 23;
        public const uint Iceland = 24;
        public const uint Iran = 161;
        public const uint Iraq = 162;
        public const uint Israel = 26;
        public const uint Italy = 27;
        public const uint IvoryCoast = 108;
        public const uint Jamaica = 82;
        public const uint Japan = 163;
        public const uint Jordan = 164;
        public const uint Kazakhstan = 165;
        public const uint Kenya = 120;
        public const uint KoreaDpr = 166;
        public const uint KoreaRepublic = 167;
        public const uint Kosovo = 219;
        public const uint Kuwait = 168;
        public const uint Latvia = 28;
        public const uint Lebanon = 171;
        public const uint Liberia = 122;
        public const uint Libya = 123;
        public const uint Liechtenstein = 29;
        public const uint Lithuania = 30;
        public const uint Luxembourg = 31;
        public const uint Madagascar = 124;
        public const uint Mali = 126;
        public const uint Malta = 32;
        public const uint Mauritania = 127;
        public const uint Mexico = 83;
        public const uint Moldova = 33;
        public const uint Montenegro = 15;
        public const uint Morocco = 129;
        public const uint Mozambique = 130;
        public const uint Netherlands = 34;
        public const uint NewCaledonia = 215;
        public const uint NewZealand = 198;
        public const uint Niger = 132;
        public const uint Nigeria = 133;
        public const uint NorthernIreland = 35;
        public const uint Norway = 36;
        public const uint Oman = 178;
        public const uint Palestine = 180;
        public const uint Panama = 87;
        public const uint Paraguay = 58;
        public const uint Peru = 59;
        public const uint Philippines = 181;
        public const uint Poland = 37;
        public const uint Portugal = 38;
        public const uint PuertoRico = 88;
        public const uint RepublicOfIreland = 25;
        public const uint Romania = 39;
        public const uint Russia = 40;
        public const uint SaudiArabia = 183;
        public const uint Scotland = 42;
        public const uint Senegal = 136;
        public const uint Serbia = 51;
        public const uint SierraLeone = 138;
        public const uint Slovakia = 43;
        public const uint Slovenia = 44;
        public const uint SouthAfrica = 140;
        public const uint Spain = 45;
        public const uint StKitts = 89;
        public const uint StLucia = 90;
        public const uint Suriname = 92;
        public const uint Sweden = 46;
        public const uint Switzerland = 47;
        public const uint Syria = 186;
        public const uint Thailand = 188;
        public const uint Togo = 144;
        public const uint TrinidadAndTobago = 93;
        public const uint Tunisia = 145;
        public const uint Turkey = 48;
        public const uint Turkmenistan = 189;
        public const uint Uganda = 146;
        public const uint Ukraine = 49;
        public const uint UnitedStates = 95;
        public const uint Uruguay = 60;
        public const uint Uzbekistan = 191;
        public const uint Venezuela = 61;
        public const uint Wales = 50;
        public const uint Zambia = 147;
        public const uint Zimbabwe = 148;

        private Nation(string description, uint value)
        {
            Description = description;
            Value = value;
        }

        public static IEnumerable<Nation> GetAll()
        {
            yield return new Nation("Albania", Albania);
            yield return new Nation("Algeria", Algeria);
            yield return new Nation("Angola", Angola);
            yield return new Nation("Antigua and Barbuda", AntiguaBarbuda);
            yield return new Nation("Argentina", Argentina);
            yield return new Nation("Armenia", Armenia);
            yield return new Nation("Australia", Australia);
            yield return new Nation("Austria", Austria);
            yield return new Nation("Azerbaijan", Azerbaijan);
            yield return new Nation("Bahrain", Bahrain);
            yield return new Nation("Barbados", Barbados);
            yield return new Nation("Belarus", Belarus);
            yield return new Nation("Belgium", Belgium);
            yield return new Nation("Benin", Benin);
            yield return new Nation("Bermuda", Bermuda);
            yield return new Nation("Bolivia", Bolivia);
            yield return new Nation("Bosnia Herzegovina", BosniaHerzegovina);
            yield return new Nation("Brazil", Brazil);
            yield return new Nation("Bulgaria", Bulgaria);
            yield return new Nation("Burkina Faso", BurkinaFaso);
            yield return new Nation("Burundi", Burundi);
            yield return new Nation("Cambodia", Cambodia);
            yield return new Nation("Cameroon", Cameroon);
            yield return new Nation("Canada", Canada);
            yield return new Nation("Cape Verde Islands", CapeVerdeIslands);
            yield return new Nation("Central African Republic", CentralAfricanRep);
            yield return new Nation("Chile", Chile);
            yield return new Nation("China PR", ChinaPr);
            yield return new Nation("Colombia", Colombia);
            yield return new Nation("Comoros", Comoros);
            yield return new Nation("Congo", Congo);
            yield return new Nation("Costa Rica", CostaRica);
            yield return new Nation("Croatia", Croatia);
            yield return new Nation("Cuba", Cuba);
            yield return new Nation("Curacao", Curacao);
            yield return new Nation("Cyprus", Cyprus);
            yield return new Nation("Czech Republic", CzechRepublic);
            yield return new Nation("Denmark", Denmark);
            yield return new Nation("DR Congo", DrCongo);
            yield return new Nation("Ecuador", Ecuador);
            yield return new Nation("Egypt", Egypt);
            yield return new Nation("El Salvador", ElSalvador);
            yield return new Nation("England", England);
            yield return new Nation("Estonia", Estonia);
            yield return new Nation("Equatorial Guinea", EquatorialGuinea);
            yield return new Nation("Faroe Islands", FaroeIslands);
            yield return new Nation("Fiji", Fiji);
            yield return new Nation("Finland", Finland);
            yield return new Nation("France", France);
            yield return new Nation("French Guiana", FrenchGuiana);
            yield return new Nation("FYR Macedonia", FyrMacedonia);
            yield return new Nation("Gabon", Gabon);
            yield return new Nation("Gambia", Gambia);
            yield return new Nation("Georgia", Georgia);
            yield return new Nation("Germany", Germany);
            yield return new Nation("Ghana", Ghana);
            yield return new Nation("Greece", Greece);
            yield return new Nation("Grenada", Grenada);
            yield return new Nation("Guam", Guam);
            yield return new Nation("Guatemala", Guatemala);
            yield return new Nation("Guinea", Guinea);
            yield return new Nation("Guinea Bissau", GuineaBissau);
            yield return new Nation("Haiti", Haiti);
            yield return new Nation("Honduras", Honduras);
            yield return new Nation("Hungary", Hungary);
            yield return new Nation("Iceland", Iceland);
            yield return new Nation("Iran", Iran);
            yield return new Nation("Iraq", Iraq);
            yield return new Nation("Israel", Israel);
            yield return new Nation("Italy", Italy);
            yield return new Nation("Ivory Coast", IvoryCoast);
            yield return new Nation("Jamaica", Jamaica);
            yield return new Nation("Japan", Japan);
            yield return new Nation("Jordan", Jordan);
            yield return new Nation("Kazakhstan", Kazakhstan);
            yield return new Nation("Kenya", Kenya);
            yield return new Nation("Korea DPR", KoreaDpr);
            yield return new Nation("Korea Republic", KoreaRepublic);
            yield return new Nation("Kosovo", Kosovo);
            yield return new Nation("Kuwait", Kuwait);
            yield return new Nation("Latvia", Latvia);
            yield return new Nation("Lebanon", Lebanon);
            yield return new Nation("Liberia", Liberia);
            yield return new Nation("Libya", Libya);
            yield return new Nation("Liechtenstein", Liechtenstein);
            yield return new Nation("Lithuania", Lithuania);
            yield return new Nation("Luxembourg", Luxembourg);
            yield return new Nation("Madagascar", Madagascar);
            yield return new Nation("Mali", Mali);
            yield return new Nation("Malta", Malta);
            yield return new Nation("Mauritania", Mauritania);
            yield return new Nation("Mexico", Mexico);
            yield return new Nation("Moldova", Moldova);
            yield return new Nation("Montenegro", Montenegro);
            yield return new Nation("Morocco", Morocco);
            yield return new Nation("Mozambique", Mozambique);
            yield return new Nation("Netherlands", Netherlands);
            yield return new Nation("New Caledonia", NewCaledonia);
            yield return new Nation("New Zealand", NewZealand);
            yield return new Nation("Niger", Niger);
            yield return new Nation("Nigeria", Nigeria);
            yield return new Nation("Northern Ireland", NorthernIreland);
            yield return new Nation("Norway", Norway);
            yield return new Nation("Oman", Oman);
            yield return new Nation("Palestine", Palestine);
            yield return new Nation("Panama", Panama);
            yield return new Nation("Paraguay", Paraguay);
            yield return new Nation("Peru", Peru);
            yield return new Nation("Philippines", Philippines);
            yield return new Nation("Poland", Poland);
            yield return new Nation("Portugal", Portugal);
            yield return new Nation("Puerto Rico", PuertoRico);
            yield return new Nation("Republic of Ireland", RepublicOfIreland);
            yield return new Nation("Romania", Romania);
            yield return new Nation("Russia", Russia);
            yield return new Nation("Saudi Arabia", SaudiArabia);
            yield return new Nation("Scotland", Scotland);
            yield return new Nation("Senegal", Senegal);
            yield return new Nation("Serbia", Serbia);
            yield return new Nation("Sierra Leone", SierraLeone);
            yield return new Nation("Slovakia", Slovakia);
            yield return new Nation("Slovenia", Slovenia);
            yield return new Nation("South Africa", SouthAfrica);
            yield return new Nation("Spain", Spain);
            yield return new Nation("St Kitts and Nevis", StKitts);
            yield return new Nation("St Lucia", StLucia);
            yield return new Nation("Suriname", Suriname);
            yield return new Nation("Sweden", Sweden);
            yield return new Nation("Switzerland", Switzerland);
            yield return new Nation("Syria", Syria);
            yield return new Nation("Thailand", Thailand);
            yield return new Nation("Togo", Togo);
            yield return new Nation("Trinidad & Tobago", TrinidadAndTobago);
            yield return new Nation("Tunisia", Tunisia);
            yield return new Nation("Turkey", Turkey);
            yield return new Nation("Turkmenistan", Turkmenistan);
            yield return new Nation("Uganda", Uganda);
            yield return new Nation("Ukraine", Ukraine);
            yield return new Nation("United States", UnitedStates);
            yield return new Nation("Uruguay", Uruguay);
            yield return new Nation("Uzbekistan", Uzbekistan);
            yield return new Nation("Venezuela", Venezuela);
            yield return new Nation("Wales", Wales);
            yield return new Nation("Zambia", Zambia);
            yield return new Nation("Zimbabwe", Zimbabwe);
        }

    }
}