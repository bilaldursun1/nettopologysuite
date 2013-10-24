﻿using NUnit.Framework;

namespace NetTopologySuite.Tests.Various
{
    using Geometries;
    using Samples.SimpleTests;

    [TestFixture]
    public class GoogleGroupTests : BaseSamples
    {
        public GoogleGroupTests() : base(GeometryFactory.Fixed) { }

        /// <summary>
        /// http://groups.google.com/group/nettopologysuite/browse_thread/thread/d423b51267b7a9fb?hl=en
        /// </summary>
        [Test]
        public void DepthMismatchAndIndexOutOfRangeAtBuffer0()
        {
            const string text =
            @"GEOMETRYCOLLECTION(POLYGON((515947.25631471083
7171484.5030738059,515947.25631471066
7171484.5030738059,515947.30958362931
7171484.5257367212,515947.36621354509
7171484.5377466371,515947.42409582069
7171484.5386563586,515947.48107518692
7171484.5284320125,515947.53502999456
7171484.5074543059,515947.58395121509
7171484.4765043529,515947.62601724768
7171484.436734586,515947.65966174705
7171484.3896258492,515947.683631947
7171484.336932255,515947.69703530765
7171484.280615869,515947.6993727496
7171484.2227736535,515947.69055723737
7171484.1655593887,515947.67091701995
7171484.1111034676,515947.64118340856
7171484.06143358,515947.602463546 7171484.0183992023,515947.6024635458
7171484.0183992023,515930.73246370832
7171468.5683991965,515930.7324637085
7171468.5683991965,515930.68105448957
7171468.530531059,515930.62279574666
7171468.5044001872,515930.56032653846
7171468.49119028,515930.49647665321
7171468.4914997332,515930.43413842231
7171468.5053145289,515923.33413849055
7171470.8953145277,515923.28133748128
7171470.9189756932,515923.23406070529
7171470.9523329083,515923.1940667521
7171470.9941453589,515923.16284330672
7171471.0428577168,515923.14155181107
7171471.0966579914,515923.13098426076
7171471.1535449345,515923.13153374492
7171471.2114024786,515923.143179824 7171471.2680784538,515923.16548929
7171471.321464642,515923.19763228117
7171471.3695751959,515923.23841315042
7171471.4106205134,515923.28631494159
7171471.4430737989,515947.25631471083
7171484.5030738059)),POLYGON((515930.67750898772
7171468.5284942714,515930.67750898789
7171468.5284942714,515930.62430464325
7171468.5048963027,515930.56754495931
7171468.4920159038,515930.50936634938
7171468.4903378887,515930.4519586346
7171468.499925416,515930.39748261991
7171468.5204176148,515930.34798876219
7171468.5510431668,515930.30533999176
7171468.5906493375,515930.27114159235
7171468.6377453655,515930.24668077903
7171468.6905585742,515930.23287824768
7171468.7471010936,515930.23025352036
7171468.8052446852,515930.23890539084
7171468.8628008449,515930.258508206
7171468.9176031807,515930.28832412313
7171468.9675889537,515930.32723088242
7171469.010876717,515947.1972307199
7171484.4608767228,515947.19723071973
7171484.4608767228,515947.24561931624
7171484.496958348,515947.30025129457
7171484.5226232428,515947.35891506722
7171484.5368324546,515947.41923583351
7171484.5390107706,515947.47877171508
7171484.52907001,515947.53511260683 7171484.507412591,515947.585977742
7171484.4749152381,515947.6293080207
7171484.4328934951,515947.66334936587
7171484.3830484683,515950.12334934279
7171479.8630484659,515950.12334934284
7171479.8630484659,515950.14608284581
7171479.8094661487,515950.158041555
7171479.7525024218,515950.15877530782
7171479.6943015754,515950.14825648355
7171479.6370544685,515950.12688104308
7171479.5829160586,515950.09545362374
7171479.5339242816,515950.05515725032
7171479.4919233387,515950.00750880211
7171479.4584942749,515930.67750898772
7171468.5284942714)),POLYGON((515939.1569354072
7171453.1813345412,515939.15693540714
7171453.1813345412,515939.15930770477
7171453.1216542246,515939.14981016068
7171453.0626867367,515939.12881923292
7171453.0067693973,515939.0971669479
7171452.9561186247,515939.05610792048
7171452.9127420839,515939.00726962468
7171452.878359112,515938.95258788462
7171452.8543325588,515938.89423014346
7171452.8416147754,515938.83450955164
7171452.8407098632,515938.77579327946
7171452.8516536895,515938.72040868853
7171452.8740124693,515938.67055108119
7171452.9068999579,515938.62819668453
7171452.9490125813,515938.59502431774
7171452.9986811029,515930.26502439909
7171468.648681107,515930.26502439892
7171468.648681107,515930.24259987607
7171468.70309941,515930.23123186041
7171468.760848688,515930.2313579208
7171468.819706101,515930.24297320505
7171468.8774061529,515930.26563062659
7171468.9317279,515930.29845807329 7171468.98058043,515930.34019197628
7171469.0220833477,515930.38922594627
7171469.0546391569,515930.44367260527
7171469.0769947432,515930.50143623422
7171469.0882896129,515930.56029344
7171469.0880890116,515930.61797873606
7171469.0764006618,515937.02797867375
7171467.1064006621,515937.02797867381
7171467.1064006621,515937.08024938527
7171467.0847553564,515937.12752685562
7171467.0536814556,515937.16812971595
7171467.0142840669,515937.20061397221
7171466.967964313,515937.22382435924
7171466.9163695024,515937.23693542625
7171466.8613345455,515939.1569354072
7171453.1813345412)),POLYGON((515936.64275903988
7171466.7779413685,515936.64275903994
7171466.7779413685,515936.64037420205
7171466.8374116365,515936.64977662347
7171466.8961823331,515936.6705962249
7171466.9519402422,515936.70201354678
7171467.0024907328,515936.74279200315
7171467.0458441386,515936.79132655397
7171467.080294067,515936.8457068791
7171467.1044845711,515936.90379256883
7171467.1174635123,515936.96329737024
7171467.1187200379,515937.02187917416
7171467.1082046926,515937.07723220065
7171467.0863313591,515937.12717775448
7171467.0539609725,515937.16974997846
7171467.0123676322,515937.20327322971
7171466.9631884508,515937.20327322959
7171466.9631884508,515945.81327314617
7171451.1631884472,515945.81327314628
7171451.1631884472,515945.83636858669
7171451.1085506417,515945.84826202842
7171451.0504366569,515945.848488479
7171450.9911185494,515945.83703908487
7171450.9329154519,515945.81436147797
7171450.8781029042,515945.78134227433
7171450.8288238887,515945.73927241081
7171450.7870050445,515945.68979667372
7171450.754281343,515945.63484939357
7171450.7319321688,515945.57657881925
7171450.7208312973,515945.517263129
7171450.7214127341,515945.45922136132
7171450.7336537465,515938.7692214257
7171452.8536537457,515938.76922142575
7171452.8536537457,515938.71752445213
7171452.8755466212,515938.67081174027
7171452.9066881947,515938.63071920839
7171452.9459878635,515938.59865093039
7171452.99206932,515938.57572996413
7171453.0433187522,515938.56275902083
7171453.0979413642,515936.64275903988
7171466.7779413685)),POLYGON((515936.44746216031
7171445.9830631334,515936.39171511418
7171445.9610246262,515936.33269933035
7171445.95050958,515936.27277113119
7171445.9519378291,515936.21432326932
7171445.9652523464,515936.15968939179
7171445.9899215251,515936.1110508648
7171446.0249603987,515936.07034967822
7171446.06896997,515936.03921090788
7171446.1201930689,515936.01887783111
7171446.1765845129,515936.0101622864
7171446.2358927606,515936.01341225923
7171446.2957498124,515936.02849798783
7171446.3537657559,515938.5784979646
7171453.2437657584,515938.57849796471
7171453.2437657584,515938.60328883695
7171453.2951300722,515938.63735252729
7171453.3408744168,515938.679457868
7171453.3793454487,515938.728083041
7171453.4091527071,515938.78147058113
7171453.4292188622,515938.83769089676
7171453.4388186634,515938.89471201063
7171453.4376051417,515938.95047300233
7171453.42562216,515945.64047293796 7171451.30562216,515945.6404729379
7171451.30562216,515945.6945674374 7171451.28242311,515945.74309115321
7171451.2491085511,515945.78417624
7171451.2069608765,515945.81624119077
7171451.1576024955,515945.83805171488
7171451.1029333826,515945.84876824991
7171451.0450579422,515945.84797827946
7171450.9862039974,515945.8357122122
7171450.9286370417,515945.81244221143
7171450.8745730249,515945.77906401979
7171450.8260930581,515945.73686247919
7171450.7850633031,515945.6874620727
7171450.7530631367,515936.44746216031
7171445.9830631334)),POLYGON((515949.71218541742
7171479.9807816455,515949.71218541724
7171479.9807816455,515949.764571373
7171480.0041068234,515949.82043844188
7171480.0170382913,515949.877745395
7171480.01910357,515949.93439839408 7171480.0102272,515949.98832749442
7171479.9907334987,515950.03756227519
7171479.9613347119,515950.08030383306
7171479.9231049921,515950.11499050906
7171479.8774411473,515950.14035494759
7171479.8260116074,515950.15547040192
7171479.7706954684,515950.1597845949
7171479.7135138279,515950.15313989786
7171479.6565559469,515950.13577908964
7171479.6019029086,515950.1083364865
7171479.5515515851,515950.07181476551
7171479.5073416745,515937.15181488881
7171466.6073416714,515937.11026802537
7171466.5727437418,515937.06318597303
7171466.5461648228,515937.01209793513
7171466.5284681851,515936.95866322768
7171466.5202286076,515936.90461738536
7171466.5217137085,515936.85171579238
7171466.532875252,515930.44171585469
7171468.5028752517,515930.44171585463
7171468.5028752517,515930.38806781213
7171468.5252545644,515930.33974271663
7171468.5575598469,515930.29855487816
7171468.5985782342,515930.26605064649
7171468.6467697378,515930.24345035554
7171468.7003250634,515930.23160250706
7171468.75723354,515930.230951915
7171468.8153586015,515930.24152300489
7171468.8725180114,515930.2629188975
7171468.9265657859,515930.29433630867
7171468.9754727632,515930.334595708
7171469.0174027868,515930.38218560285
7171469.0507816421,515930.382185603
7171469.0507816421,515949.71218541742
7171479.9807816455)),POLYGON((515937.08338285086
7171466.5562038543,515937.0833828508
7171466.5562038543,515937.03093628137
7171466.5338009661,515936.97519782244
7171466.5217280108,515936.91818181996
7171466.5204212964,515936.86194878916
7171466.5299280463,515936.80853094929
7171466.5499046929,515936.75985878077
7171466.5796292964,515936.717691259
7171466.6180276321,515936.68355228635
7171466.6637120126,515936.65867561958
7171466.7150314385,515936.64396028261
7171466.7701312648,515936.63993807667
7171466.827020226,515936.64675436111
7171466.8836423978,515936.66416280065
7171466.9379514968,515936.69153426745
7171466.9879848342,515936.72787957732
7171467.0319342427,515949.647879454
7171479.9319342459,515949.69586931693
7171479.9708575523,515949.7508155746
7171479.9991234234,515949.81038725376
7171480.0155327432,515949.8720571578
7171480.0193893816,515949.93320907763
7171480.01052973,515949.99124877836
7171479.9893296389,515950.04371405422
7171479.9566884758,515950.0883791821
7171479.9139909688,515950.12334934284
7171479.8630484659,515950.12334934279
7171479.8630484659,515952.58334931784
7171475.3430484664,515952.58334931789
7171475.3430484664,515952.6062589
7171475.2889031395,515952.61816846911
7171475.2313294681,515952.61862062296
7171475.1725386446,515952.60759799596
7171475.1147886049,515952.58552392625
7171475.0602973131,515952.55324619677
7171475.0111575779,515952.51200447517
7171474.9692566739,515952.46338270261
7171474.9362038579,515937.08338285086
7171466.5562038543)),POLYGON((515936.60044480651
7171446.3241567584,515936.60044480645
7171446.3241567584,515936.60936612997
7171446.2666212628,515936.60700583045
7171446.2084460761,515936.59345281048
7171446.1518224152,515936.56921755557
7171446.0988830561,515936.53521290637
7171446.0516220033,515936.49271967571
7171446.0118193822,515936.44333840563
7171445.9809743911,515936.388929082
7171445.9602488317,515936.3315410763
7171445.95042335,515936.27333595447
7171445.9518680284,515936.21650605986
7171445.9645284545,515936.16319193668
7171445.9879277619,515936.11540170485
7171446.0211845962,515936.07493542245
7171446.0630463129,515936.04331728484
7171446.1119361548,515936.04331728473
7171446.1119361548,515923.16331741109
7171471.04193616,515923.16331741121
7171471.04193616,515923.14146267105
7171471.0969683174,515923.13084265508
7171471.15522107,515923.13187109178
7171471.2144250423,515923.14450791589
7171471.2722737994,515923.16826082935
7171471.326513703,515923.20220447978
7171471.375031705,515923.24501651019
7171471.4159376714,515923.29502907419
7171471.44763801,515923.35029381118
7171471.4688977562,515923.40865774947
7171471.4788886867,515923.46784718055
7171471.47722158,515923.52555623668
7171471.4639613833,515930.62555616844
7171469.0739613846,515930.67987846828
7171469.0494275773,515930.728278171
7171469.0146383736,515930.76884466066
7171468.9709671037,515930.79997654364
7171468.9201377239,515930.82044486463
7171468.8641567649,515936.60044480651
7171446.3241567584)),POLYGON((515930.23924972612
7171468.7151191486,515930.23924972618
7171468.7151191486,515930.2303423743
7171468.7724100072,515930.23262180877
7171468.8303443454,515930.24600289058
7171468.8867582632,515930.26998582395
7171468.939544647,515930.30367482413
7171468.9867318766,515930.34581157565
7171469.026557466,515930.39482223184
7171469.0575338919,515930.44887619931
7171469.0785041573,515930.50595451222
7171469.0886850012,515930.56392524275
7171469.0876961621,515930.62062313035
7171469.0755745722,515930.6739304564
7171469.0527729839,515930.72185614286
7171469.02014306,515930.76261012093
7171468.9789035562,515930.79467019183
7171468.9305948066,515930.79467019165
7171468.9305948066,515939.12467011029
7171453.2805948025,515939.12467011047
7171453.2805948025,515939.14831025776
7171453.22203378,515939.15916748613
7171453.1598214786,515939.15676066949
7171453.0967147658,515939.14119646343
7171453.035510147,515936.59119648667
7171446.1455101445,515936.59119648655
7171446.1455101445,515936.56567452877
7171446.0929459054,515936.53044713859
7171446.0463261614,515936.48685075296
7171446.0074195461,515936.43653930712
7171445.9777020765,515936.38142148854
7171445.9583011577,515936.32358832611
7171445.949952811,515936.26523386192
7171445.9529737514,515936.208571915
7171445.967249372,515936.15575209452
7171445.9922380922,515936.10877824918
7171446.0269919038,515936.06943244644
7171446.0701923361,515936.03920736536
7171446.1202004757,515936.019249668
7171446.175119142,515930.23924972612 7171468.7151191486)))";

            var coll = Reader.Read(text);
            Assert.IsNotNull(coll);

            var union = coll.Buffer(0.0);
            Assert.IsNotNull(union);
        }
    }
}