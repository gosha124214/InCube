; ModuleID = 'marshal_methods.x86.ll'
source_filename = "marshal_methods.x86.ll"
target datalayout = "e-m:e-p:32:32-p270:32:32-p271:32:32-p272:64:64-f64:32:64-f80:32-n8:16:32-S128"
target triple = "i686-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [347 x ptr] zeroinitializer, align 4

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [688 x i32] [
	i32 2616222, ; 0: System.Net.NetworkInformation.dll => 0x27eb9e => 67
	i32 10166715, ; 1: System.Net.NameResolution.dll => 0x9b21bb => 66
	i32 15721112, ; 2: System.Runtime.Intrinsics.dll => 0xefe298 => 107
	i32 26230656, ; 3: Microsoft.Extensions.DependencyModel => 0x1903f80 => 192
	i32 32687329, ; 4: Xamarin.AndroidX.Lifecycle.Runtime => 0x1f2c4e1 => 262
	i32 34715100, ; 5: Xamarin.Google.Guava.ListenableFuture.dll => 0x211b5dc => 296
	i32 34839235, ; 6: System.IO.FileSystem.DriveInfo => 0x2139ac3 => 47
	i32 39109920, ; 7: Newtonsoft.Json.dll => 0x254c520 => 206
	i32 39485524, ; 8: System.Net.WebSockets.dll => 0x25a8054 => 79
	i32 42639949, ; 9: System.Threading.Thread => 0x28aa24d => 142
	i32 66541672, ; 10: System.Diagnostics.StackTrace => 0x3f75868 => 29
	i32 67008169, ; 11: zh-Hant\Microsoft.Maui.Controls.resources => 0x3fe76a9 => 339
	i32 68219467, ; 12: System.Security.Cryptography.Primitives => 0x410f24b => 123
	i32 72070932, ; 13: Microsoft.Maui.Graphics.dll => 0x44bb714 => 203
	i32 82292897, ; 14: System.Runtime.CompilerServices.VisualC.dll => 0x4e7b0a1 => 101
	i32 101534019, ; 15: Xamarin.AndroidX.SlidingPaneLayout => 0x60d4943 => 280
	i32 117431740, ; 16: System.Runtime.InteropServices => 0x6ffddbc => 106
	i32 120558881, ; 17: Xamarin.AndroidX.SlidingPaneLayout.dll => 0x72f9521 => 280
	i32 122350210, ; 18: System.Threading.Channels.dll => 0x74aea82 => 136
	i32 134690465, ; 19: Xamarin.Kotlin.StdLib.Jdk7.dll => 0x80736a1 => 300
	i32 142721839, ; 20: System.Net.WebHeaderCollection => 0x881c32f => 76
	i32 149972175, ; 21: System.Security.Cryptography.Primitives.dll => 0x8f064cf => 123
	i32 159306688, ; 22: System.ComponentModel.Annotations => 0x97ed3c0 => 13
	i32 165246403, ; 23: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 236
	i32 176265551, ; 24: System.ServiceProcess => 0xa81994f => 131
	i32 182336117, ; 25: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 282
	i32 184328833, ; 26: System.ValueTuple.dll => 0xafca281 => 148
	i32 195452805, ; 27: vi/Microsoft.Maui.Controls.resources.dll => 0xba65f85 => 336
	i32 199333315, ; 28: zh-HK/Microsoft.Maui.Controls.resources.dll => 0xbe195c3 => 337
	i32 205061960, ; 29: System.ComponentModel => 0xc38ff48 => 18
	i32 209399409, ; 30: Xamarin.AndroidX.Browser.dll => 0xc7b2e71 => 234
	i32 220171995, ; 31: System.Diagnostics.Debug => 0xd1f8edb => 26
	i32 230216969, ; 32: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 0xdb8d509 => 256
	i32 230752869, ; 33: Microsoft.CSharp.dll => 0xdc10265 => 1
	i32 231409092, ; 34: System.Linq.Parallel => 0xdcb05c4 => 58
	i32 231814094, ; 35: System.Globalization => 0xdd133ce => 41
	i32 246610117, ; 36: System.Reflection.Emit.Lightweight => 0xeb2f8c5 => 90
	i32 261689757, ; 37: Xamarin.AndroidX.ConstraintLayout.dll => 0xf99119d => 239
	i32 276479776, ; 38: System.Threading.Timer.dll => 0x107abf20 => 144
	i32 278686392, ; 39: Xamarin.AndroidX.Lifecycle.LiveData.dll => 0x109c6ab8 => 258
	i32 280482487, ; 40: Xamarin.AndroidX.Interpolator => 0x10b7d2b7 => 255
	i32 280992041, ; 41: cs/Microsoft.Maui.Controls.resources.dll => 0x10bf9929 => 308
	i32 291076382, ; 42: System.IO.Pipes.AccessControl.dll => 0x1159791e => 53
	i32 298918909, ; 43: System.Net.Ping.dll => 0x11d123fd => 68
	i32 317674968, ; 44: vi\Microsoft.Maui.Controls.resources => 0x12ef55d8 => 336
	i32 318968648, ; 45: Xamarin.AndroidX.Activity.dll => 0x13031348 => 225
	i32 321597661, ; 46: System.Numerics => 0x132b30dd => 82
	i32 336156722, ; 47: ja/Microsoft.Maui.Controls.resources.dll => 0x14095832 => 321
	i32 342366114, ; 48: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 257
	i32 347068432, ; 49: SQLitePCLRaw.lib.e_sqlite3.android.dll => 0x14afd810 => 210
	i32 356389973, ; 50: it/Microsoft.Maui.Controls.resources.dll => 0x153e1455 => 320
	i32 360082299, ; 51: System.ServiceModel.Web => 0x15766b7b => 130
	i32 367780167, ; 52: System.IO.Pipes => 0x15ebe147 => 54
	i32 374914964, ; 53: System.Transactions.Local => 0x1658bf94 => 146
	i32 375677976, ; 54: System.Net.ServicePoint.dll => 0x16646418 => 73
	i32 379916513, ; 55: System.Threading.Thread.dll => 0x16a510e1 => 142
	i32 385762202, ; 56: System.Memory.dll => 0x16fe439a => 61
	i32 392610295, ; 57: System.Threading.ThreadPool.dll => 0x1766c1f7 => 143
	i32 395744057, ; 58: _Microsoft.Android.Resource.Designer => 0x17969339 => 343
	i32 403441872, ; 59: WindowsBase => 0x180c08d0 => 162
	i32 435591531, ; 60: sv/Microsoft.Maui.Controls.resources.dll => 0x19f6996b => 332
	i32 441335492, ; 61: Xamarin.AndroidX.ConstraintLayout.Core => 0x1a4e3ec4 => 240
	i32 442565967, ; 62: System.Collections => 0x1a61054f => 12
	i32 450948140, ; 63: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 253
	i32 451504562, ; 64: System.Security.Cryptography.X509Certificates => 0x1ae969b2 => 124
	i32 456227837, ; 65: System.Web.HttpUtility.dll => 0x1b317bfd => 149
	i32 459347974, ; 66: System.Runtime.Serialization.Primitives.dll => 0x1b611806 => 112
	i32 465846621, ; 67: mscorlib => 0x1bc4415d => 163
	i32 469710990, ; 68: System.dll => 0x1bff388e => 161
	i32 476646585, ; 69: Xamarin.AndroidX.Interpolator.dll => 0x1c690cb9 => 255
	i32 486930444, ; 70: Xamarin.AndroidX.LocalBroadcastManager.dll => 0x1d05f80c => 268
	i32 498788369, ; 71: System.ObjectModel => 0x1dbae811 => 83
	i32 500358224, ; 72: id/Microsoft.Maui.Controls.resources.dll => 0x1dd2dc50 => 319
	i32 503918385, ; 73: fi/Microsoft.Maui.Controls.resources.dll => 0x1e092f31 => 313
	i32 513247710, ; 74: Microsoft.Extensions.Primitives.dll => 0x1e9789de => 197
	i32 526420162, ; 75: System.Transactions.dll => 0x1f6088c2 => 147
	i32 527452488, ; 76: Xamarin.Kotlin.StdLib.Jdk7 => 0x1f704948 => 300
	i32 530272170, ; 77: System.Linq.Queryable => 0x1f9b4faa => 59
	i32 539058512, ; 78: Microsoft.Extensions.Logging => 0x20216150 => 193
	i32 540030774, ; 79: System.IO.FileSystem.dll => 0x20303736 => 50
	i32 545304856, ; 80: System.Runtime.Extensions => 0x2080b118 => 102
	i32 546455878, ; 81: System.Runtime.Serialization.Xml => 0x20924146 => 113
	i32 549171840, ; 82: System.Globalization.Calendars => 0x20bbb280 => 39
	i32 557405415, ; 83: Jsr305Binding => 0x213954e7 => 293
	i32 569601784, ; 84: Xamarin.AndroidX.Window.Extensions.Core.Core => 0x21f36ef8 => 291
	i32 577335427, ; 85: System.Security.Cryptography.Cng => 0x22697083 => 119
	i32 592146354, ; 86: pt-BR/Microsoft.Maui.Controls.resources.dll => 0x234b6fb2 => 327
	i32 601371474, ; 87: System.IO.IsolatedStorage.dll => 0x23d83352 => 51
	i32 605376203, ; 88: System.IO.Compression.FileSystem => 0x24154ecb => 43
	i32 613668793, ; 89: System.Security.Cryptography.Algorithms => 0x2493d7b9 => 118
	i32 618636221, ; 90: K4os.Compression.LZ4.Streams => 0x24dfa3bd => 179
	i32 627609679, ; 91: Xamarin.AndroidX.CustomView => 0x2568904f => 245
	i32 627931235, ; 92: nl\Microsoft.Maui.Controls.resources => 0x256d7863 => 325
	i32 639843206, ; 93: Xamarin.AndroidX.Emoji2.ViewsHelper.dll => 0x26233b86 => 251
	i32 643868501, ; 94: System.Net => 0x2660a755 => 80
	i32 662205335, ; 95: System.Text.Encodings.Web.dll => 0x27787397 => 218
	i32 663517072, ; 96: Xamarin.AndroidX.VersionedParcelable => 0x278c7790 => 287
	i32 666292255, ; 97: Xamarin.AndroidX.Arch.Core.Common.dll => 0x27b6d01f => 232
	i32 672442732, ; 98: System.Collections.Concurrent => 0x2814a96c => 8
	i32 683518922, ; 99: System.Net.Security => 0x28bdabca => 72
	i32 688181140, ; 100: ca/Microsoft.Maui.Controls.resources.dll => 0x2904cf94 => 307
	i32 690569205, ; 101: System.Xml.Linq.dll => 0x29293ff5 => 152
	i32 691348768, ; 102: Xamarin.KotlinX.Coroutines.Android.dll => 0x29352520 => 302
	i32 693804605, ; 103: System.Windows => 0x295a9e3d => 151
	i32 699345723, ; 104: System.Reflection.Emit => 0x29af2b3b => 91
	i32 700284507, ; 105: Xamarin.Jetbrains.Annotations => 0x29bd7e5b => 297
	i32 700358131, ; 106: System.IO.Compression.ZipFile => 0x29be9df3 => 44
	i32 706645707, ; 107: ko/Microsoft.Maui.Controls.resources.dll => 0x2a1e8ecb => 322
	i32 709557578, ; 108: de/Microsoft.Maui.Controls.resources.dll => 0x2a4afd4a => 310
	i32 720511267, ; 109: Xamarin.Kotlin.StdLib.Jdk8 => 0x2af22123 => 301
	i32 722857257, ; 110: System.Runtime.Loader.dll => 0x2b15ed29 => 108
	i32 735137430, ; 111: System.Security.SecureString.dll => 0x2bd14e96 => 128
	i32 748085224, ; 112: de-DE/FluentMigrator.Abstractions.resources.dll => 0x2c96dfe8 => 305
	i32 748832960, ; 113: SQLitePCLRaw.batteries_v2 => 0x2ca248c0 => 208
	i32 752232764, ; 114: System.Diagnostics.Contracts.dll => 0x2cd6293c => 25
	i32 755313932, ; 115: Xamarin.Android.Glide.Annotations.dll => 0x2d052d0c => 222
	i32 759454413, ; 116: System.Net.Requests => 0x2d445acd => 71
	i32 762598435, ; 117: System.IO.Pipes.dll => 0x2d745423 => 54
	i32 775507847, ; 118: System.IO.Compression => 0x2e394f87 => 45
	i32 777317022, ; 119: sk\Microsoft.Maui.Controls.resources => 0x2e54ea9e => 331
	i32 789151979, ; 120: Microsoft.Extensions.Options => 0x2f0980eb => 196
	i32 790371945, ; 121: Xamarin.AndroidX.CustomView.PoolingContainer.dll => 0x2f1c1e69 => 246
	i32 804715423, ; 122: System.Data.Common => 0x2ff6fb9f => 22
	i32 807930345, ; 123: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx.dll => 0x302809e9 => 260
	i32 823281589, ; 124: System.Private.Uri.dll => 0x311247b5 => 85
	i32 830298997, ; 125: System.IO.Compression.Brotli => 0x317d5b75 => 42
	i32 832635846, ; 126: System.Xml.XPath.dll => 0x31a103c6 => 157
	i32 834051424, ; 127: System.Net.Quic => 0x31b69d60 => 70
	i32 843511501, ; 128: Xamarin.AndroidX.Print => 0x3246f6cd => 273
	i32 873119928, ; 129: Microsoft.VisualBasic => 0x340ac0b8 => 3
	i32 877678880, ; 130: System.Globalization.dll => 0x34505120 => 41
	i32 878954865, ; 131: System.Net.Http.Json => 0x3463c971 => 62
	i32 904024072, ; 132: System.ComponentModel.Primitives.dll => 0x35e25008 => 16
	i32 911108515, ; 133: System.IO.MemoryMappedFiles.dll => 0x364e69a3 => 52
	i32 926902833, ; 134: tr/Microsoft.Maui.Controls.resources.dll => 0x373f6a31 => 334
	i32 928116545, ; 135: Xamarin.Google.Guava.ListenableFuture => 0x3751ef41 => 296
	i32 952186615, ; 136: System.Runtime.InteropServices.JavaScript.dll => 0x38c136f7 => 104
	i32 955402788, ; 137: Newtonsoft.Json => 0x38f24a24 => 206
	i32 956575887, ; 138: Xamarin.Kotlin.StdLib.Jdk8.dll => 0x3904308f => 301
	i32 966729478, ; 139: Xamarin.Google.Crypto.Tink.Android => 0x399f1f06 => 294
	i32 967690846, ; 140: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 257
	i32 975236339, ; 141: System.Diagnostics.Tracing => 0x3a20ecf3 => 33
	i32 975874589, ; 142: System.Xml.XDocument => 0x3a2aaa1d => 155
	i32 983077409, ; 143: MySql.Data.dll => 0x3a989221 => 204
	i32 986514023, ; 144: System.Private.DataContractSerialization.dll => 0x3acd0267 => 84
	i32 987214855, ; 145: System.Diagnostics.Tools => 0x3ad7b407 => 31
	i32 992768348, ; 146: System.Collections.dll => 0x3b2c715c => 12
	i32 994442037, ; 147: System.IO.FileSystem => 0x3b45fb35 => 50
	i32 1001831731, ; 148: System.IO.UnmanagedMemoryStream.dll => 0x3bb6bd33 => 55
	i32 1012816738, ; 149: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 277
	i32 1019214401, ; 150: System.Drawing => 0x3cbffa41 => 35
	i32 1028951442, ; 151: Microsoft.Extensions.DependencyInjection.Abstractions => 0x3d548d92 => 191
	i32 1029334545, ; 152: da/Microsoft.Maui.Controls.resources.dll => 0x3d5a6611 => 309
	i32 1031528504, ; 153: Xamarin.Google.ErrorProne.Annotations.dll => 0x3d7be038 => 295
	i32 1035644815, ; 154: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 230
	i32 1036536393, ; 155: System.Drawing.Primitives.dll => 0x3dc84a49 => 34
	i32 1044663988, ; 156: System.Linq.Expressions.dll => 0x3e444eb4 => 57
	i32 1052210849, ; 157: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 264
	i32 1067306892, ; 158: GoogleGson => 0x3f9dcf8c => 177
	i32 1082857460, ; 159: System.ComponentModel.TypeConverter => 0x408b17f4 => 17
	i32 1084122840, ; 160: Xamarin.Kotlin.StdLib => 0x409e66d8 => 298
	i32 1089913930, ; 161: System.Diagnostics.EventLog.dll => 0x40f6c44a => 214
	i32 1098259244, ; 162: System => 0x41761b2c => 161
	i32 1118262833, ; 163: ko\Microsoft.Maui.Controls.resources => 0x42a75631 => 322
	i32 1121599056, ; 164: Xamarin.AndroidX.Lifecycle.Runtime.Ktx.dll => 0x42da3e50 => 263
	i32 1127624469, ; 165: Microsoft.Extensions.Logging.Debug => 0x43362f15 => 195
	i32 1145483052, ; 166: System.Windows.Extensions.dll => 0x4446af2c => 220
	i32 1149092582, ; 167: Xamarin.AndroidX.Window => 0x447dc2e6 => 290
	i32 1157931901, ; 168: Microsoft.EntityFrameworkCore.Abstractions => 0x4504a37d => 183
	i32 1157939332, ; 169: FluentMigrator.Abstractions.dll => 0x4504c084 => 172
	i32 1168523401, ; 170: pt\Microsoft.Maui.Controls.resources => 0x45a64089 => 328
	i32 1170634674, ; 171: System.Web.dll => 0x45c677b2 => 150
	i32 1175144683, ; 172: Xamarin.AndroidX.VectorDrawable.Animated => 0x460b48eb => 286
	i32 1178241025, ; 173: Xamarin.AndroidX.Navigation.Runtime.dll => 0x463a8801 => 271
	i32 1202000627, ; 174: Microsoft.EntityFrameworkCore.Abstractions.dll => 0x47a512f3 => 183
	i32 1203215381, ; 175: pl/Microsoft.Maui.Controls.resources.dll => 0x47b79c15 => 326
	i32 1204270330, ; 176: Xamarin.AndroidX.Arch.Core.Common => 0x47c7b4fa => 232
	i32 1204575371, ; 177: Microsoft.Extensions.Caching.Memory.dll => 0x47cc5c8b => 187
	i32 1208641965, ; 178: System.Diagnostics.Process => 0x480a69ad => 28
	i32 1219128291, ; 179: System.IO.IsolatedStorage => 0x48aa6be3 => 51
	i32 1234928153, ; 180: nb/Microsoft.Maui.Controls.resources.dll => 0x499b8219 => 324
	i32 1243150071, ; 181: Xamarin.AndroidX.Window.Extensions.Core.Core.dll => 0x4a18f6f7 => 291
	i32 1253011324, ; 182: Microsoft.Win32.Registry => 0x4aaf6f7c => 5
	i32 1260983243, ; 183: cs\Microsoft.Maui.Controls.resources => 0x4b2913cb => 308
	i32 1264511973, ; 184: Xamarin.AndroidX.Startup.StartupRuntime.dll => 0x4b5eebe5 => 281
	i32 1267360935, ; 185: Xamarin.AndroidX.VectorDrawable => 0x4b8a64a7 => 285
	i32 1273260888, ; 186: Xamarin.AndroidX.Collection.Ktx => 0x4be46b58 => 237
	i32 1275534314, ; 187: Xamarin.KotlinX.Coroutines.Android => 0x4c071bea => 302
	i32 1278448581, ; 188: Xamarin.AndroidX.Annotation.Jvm => 0x4c3393c5 => 229
	i32 1292207520, ; 189: SQLitePCLRaw.core.dll => 0x4d0585a0 => 209
	i32 1293217323, ; 190: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 248
	i32 1309188875, ; 191: System.Private.DataContractSerialization => 0x4e08a30b => 84
	i32 1322716291, ; 192: Xamarin.AndroidX.Window.dll => 0x4ed70c83 => 290
	i32 1324164729, ; 193: System.Linq => 0x4eed2679 => 60
	i32 1335329327, ; 194: System.Runtime.Serialization.Json.dll => 0x4f97822f => 111
	i32 1364015309, ; 195: System.IO => 0x514d38cd => 56
	i32 1373134921, ; 196: zh-Hans\Microsoft.Maui.Controls.resources => 0x51d86049 => 338
	i32 1376866003, ; 197: Xamarin.AndroidX.SavedState => 0x52114ed3 => 277
	i32 1379779777, ; 198: System.Resources.ResourceManager => 0x523dc4c1 => 98
	i32 1402170036, ; 199: System.Configuration.dll => 0x53936ab4 => 19
	i32 1406073936, ; 200: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 241
	i32 1408764838, ; 201: System.Runtime.Serialization.Formatters.dll => 0x53f80ba6 => 110
	i32 1411638395, ; 202: System.Runtime.CompilerServices.Unsafe => 0x5423e47b => 100
	i32 1422545099, ; 203: System.Runtime.CompilerServices.VisualC => 0x54ca50cb => 101
	i32 1430672901, ; 204: ar\Microsoft.Maui.Controls.resources => 0x55465605 => 306
	i32 1434145427, ; 205: System.Runtime.Handles => 0x557b5293 => 103
	i32 1435222561, ; 206: Xamarin.Google.Crypto.Tink.Android.dll => 0x558bc221 => 294
	i32 1439761251, ; 207: System.Net.Quic.dll => 0x55d10363 => 70
	i32 1452070440, ; 208: System.Formats.Asn1.dll => 0x568cd628 => 37
	i32 1453312822, ; 209: System.Diagnostics.Tools.dll => 0x569fcb36 => 31
	i32 1457743152, ; 210: System.Runtime.Extensions.dll => 0x56e36530 => 102
	i32 1458022317, ; 211: System.Net.Security.dll => 0x56e7a7ad => 72
	i32 1461004990, ; 212: es\Microsoft.Maui.Controls.resources => 0x57152abe => 312
	i32 1461234159, ; 213: System.Collections.Immutable.dll => 0x5718a9ef => 9
	i32 1461719063, ; 214: System.Security.Cryptography.OpenSsl => 0x57201017 => 122
	i32 1462112819, ; 215: System.IO.Compression.dll => 0x57261233 => 45
	i32 1469204771, ; 216: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 231
	i32 1470490898, ; 217: Microsoft.Extensions.Primitives => 0x57a5e912 => 197
	i32 1479771757, ; 218: System.Collections.Immutable => 0x5833866d => 9
	i32 1480492111, ; 219: System.IO.Compression.Brotli.dll => 0x583e844f => 42
	i32 1487239319, ; 220: Microsoft.Win32.Primitives => 0x58a57897 => 4
	i32 1487250139, ; 221: K4os.Hash.xxHash => 0x58a5a2db => 180
	i32 1490025113, ; 222: Xamarin.AndroidX.SavedState.SavedState.Ktx.dll => 0x58cffa99 => 278
	i32 1490351284, ; 223: Microsoft.Data.Sqlite.dll => 0x58d4f4b4 => 181
	i32 1493001747, ; 224: hi/Microsoft.Maui.Controls.resources.dll => 0x58fd6613 => 316
	i32 1511525525, ; 225: MySqlConnector => 0x5a180c95 => 205
	i32 1514721132, ; 226: el/Microsoft.Maui.Controls.resources.dll => 0x5a48cf6c => 311
	i32 1536373174, ; 227: System.Diagnostics.TextWriterTraceListener => 0x5b9331b6 => 30
	i32 1543031311, ; 228: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 135
	i32 1543355203, ; 229: System.Reflection.Emit.dll => 0x5bfdbb43 => 91
	i32 1550322496, ; 230: System.Reflection.Extensions.dll => 0x5c680b40 => 92
	i32 1551623176, ; 231: sk/Microsoft.Maui.Controls.resources.dll => 0x5c7be408 => 331
	i32 1565862583, ; 232: System.IO.FileSystem.Primitives => 0x5d552ab7 => 48
	i32 1566207040, ; 233: System.Threading.Tasks.Dataflow.dll => 0x5d5a6c40 => 138
	i32 1573704789, ; 234: System.Runtime.Serialization.Json => 0x5dccd455 => 111
	i32 1580037396, ; 235: System.Threading.Overlapped => 0x5e2d7514 => 137
	i32 1582372066, ; 236: Xamarin.AndroidX.DocumentFile.dll => 0x5e5114e2 => 247
	i32 1592978981, ; 237: System.Runtime.Serialization.dll => 0x5ef2ee25 => 114
	i32 1597949149, ; 238: Xamarin.Google.ErrorProne.Annotations => 0x5f3ec4dd => 295
	i32 1601112923, ; 239: System.Xml.Serialization => 0x5f6f0b5b => 154
	i32 1603525486, ; 240: Microsoft.Maui.Controls.HotReload.Forms.dll => 0x5f93db6e => 340
	i32 1604827217, ; 241: System.Net.WebClient => 0x5fa7b851 => 75
	i32 1618516317, ; 242: System.Net.WebSockets.Client.dll => 0x6078995d => 78
	i32 1622152042, ; 243: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 267
	i32 1622358360, ; 244: System.Dynamic.Runtime => 0x60b33958 => 36
	i32 1624863272, ; 245: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 289
	i32 1635184631, ; 246: Xamarin.AndroidX.Emoji2.ViewsHelper => 0x6176eff7 => 251
	i32 1636350590, ; 247: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 244
	i32 1639515021, ; 248: System.Net.Http.dll => 0x61b9038d => 63
	i32 1639986890, ; 249: System.Text.RegularExpressions => 0x61c036ca => 135
	i32 1641389582, ; 250: System.ComponentModel.EventBasedAsync.dll => 0x61d59e0e => 15
	i32 1657153582, ; 251: System.Runtime => 0x62c6282e => 115
	i32 1658241508, ; 252: Xamarin.AndroidX.Tracing.Tracing.dll => 0x62d6c1e4 => 283
	i32 1658251792, ; 253: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 292
	i32 1670060433, ; 254: Xamarin.AndroidX.ConstraintLayout => 0x638b1991 => 239
	i32 1675553242, ; 255: System.IO.FileSystem.DriveInfo.dll => 0x63dee9da => 47
	i32 1677501392, ; 256: System.Net.Primitives.dll => 0x63fca3d0 => 69
	i32 1678508291, ; 257: System.Net.WebSockets => 0x640c0103 => 79
	i32 1679769178, ; 258: System.Security.Cryptography => 0x641f3e5a => 125
	i32 1688112883, ; 259: Microsoft.Data.Sqlite => 0x649e8ef3 => 181
	i32 1689493916, ; 260: Microsoft.EntityFrameworkCore.dll => 0x64b3a19c => 182
	i32 1691477237, ; 261: System.Reflection.Metadata => 0x64d1e4f5 => 93
	i32 1696967625, ; 262: System.Security.Cryptography.Csp => 0x6525abc9 => 120
	i32 1698840827, ; 263: Xamarin.Kotlin.StdLib.Common => 0x654240fb => 299
	i32 1701541528, ; 264: System.Diagnostics.Debug.dll => 0x656b7698 => 26
	i32 1711441057, ; 265: SQLitePCLRaw.lib.e_sqlite3.android => 0x660284a1 => 210
	i32 1720223769, ; 266: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx => 0x66888819 => 260
	i32 1726116996, ; 267: System.Reflection.dll => 0x66e27484 => 96
	i32 1728033016, ; 268: System.Diagnostics.FileVersionInfo.dll => 0x66ffb0f8 => 27
	i32 1729485958, ; 269: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 235
	i32 1736233607, ; 270: ro/Microsoft.Maui.Controls.resources.dll => 0x677cd287 => 329
	i32 1743415430, ; 271: ca\Microsoft.Maui.Controls.resources => 0x67ea6886 => 307
	i32 1744735666, ; 272: System.Transactions.Local.dll => 0x67fe8db2 => 146
	i32 1746115085, ; 273: System.IO.Pipelines.dll => 0x68139a0d => 215
	i32 1746316138, ; 274: Mono.Android.Export => 0x6816ab6a => 166
	i32 1750313021, ; 275: Microsoft.Win32.Primitives.dll => 0x6853a83d => 4
	i32 1758240030, ; 276: System.Resources.Reader.dll => 0x68cc9d1e => 97
	i32 1763938596, ; 277: System.Diagnostics.TraceSource.dll => 0x69239124 => 32
	i32 1765942094, ; 278: System.Reflection.Extensions => 0x6942234e => 92
	i32 1766324549, ; 279: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 282
	i32 1770582343, ; 280: Microsoft.Extensions.Logging.dll => 0x6988f147 => 193
	i32 1776026572, ; 281: System.Core.dll => 0x69dc03cc => 21
	i32 1777075843, ; 282: System.Globalization.Extensions.dll => 0x69ec0683 => 40
	i32 1780572499, ; 283: Mono.Android.Runtime.dll => 0x6a216153 => 167
	i32 1782862114, ; 284: ms\Microsoft.Maui.Controls.resources => 0x6a445122 => 323
	i32 1788241197, ; 285: Xamarin.AndroidX.Fragment => 0x6a96652d => 253
	i32 1793755602, ; 286: he\Microsoft.Maui.Controls.resources => 0x6aea89d2 => 315
	i32 1808609942, ; 287: Xamarin.AndroidX.Loader => 0x6bcd3296 => 267
	i32 1813058853, ; 288: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 298
	i32 1813201214, ; 289: Xamarin.Google.Android.Material => 0x6c13413e => 292
	i32 1818569960, ; 290: Xamarin.AndroidX.Navigation.UI.dll => 0x6c652ce8 => 272
	i32 1818787751, ; 291: Microsoft.VisualBasic.Core => 0x6c687fa7 => 2
	i32 1824175904, ; 292: System.Text.Encoding.Extensions => 0x6cbab720 => 133
	i32 1824722060, ; 293: System.Runtime.Serialization.Formatters => 0x6cc30c8c => 110
	i32 1827303595, ; 294: Microsoft.VisualStudio.DesignTools.TapContract => 0x6cea70ab => 342
	i32 1828688058, ; 295: Microsoft.Extensions.Logging.Abstractions.dll => 0x6cff90ba => 194
	i32 1829150748, ; 296: System.Windows.Extensions => 0x6d06a01c => 220
	i32 1842015223, ; 297: uk/Microsoft.Maui.Controls.resources.dll => 0x6dcaebf7 => 335
	i32 1847515442, ; 298: Xamarin.Android.Glide.Annotations => 0x6e1ed932 => 222
	i32 1853025655, ; 299: sv\Microsoft.Maui.Controls.resources => 0x6e72ed77 => 332
	i32 1858542181, ; 300: System.Linq.Expressions => 0x6ec71a65 => 57
	i32 1870277092, ; 301: System.Reflection.Primitives => 0x6f7a29e4 => 94
	i32 1875935024, ; 302: fr\Microsoft.Maui.Controls.resources => 0x6fd07f30 => 314
	i32 1879696579, ; 303: System.Formats.Tar.dll => 0x7009e4c3 => 38
	i32 1885316902, ; 304: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0x705fa726 => 233
	i32 1885918049, ; 305: Microsoft.VisualStudio.DesignTools.TapContract.dll => 0x7068d361 => 342
	i32 1886040351, ; 306: Microsoft.EntityFrameworkCore.Sqlite.dll => 0x706ab11f => 185
	i32 1888955245, ; 307: System.Diagnostics.Contracts => 0x70972b6d => 25
	i32 1889954781, ; 308: System.Reflection.Metadata.dll => 0x70a66bdd => 93
	i32 1897940508, ; 309: FluentMigrator.Runner.MySql => 0x7120461c => 175
	i32 1898237753, ; 310: System.Reflection.DispatchProxy => 0x7124cf39 => 88
	i32 1900610850, ; 311: System.Resources.ResourceManager.dll => 0x71490522 => 98
	i32 1910275211, ; 312: System.Collections.NonGeneric.dll => 0x71dc7c8b => 10
	i32 1925302748, ; 313: K4os.Compression.LZ4.dll => 0x72c1c9dc => 178
	i32 1939592360, ; 314: System.Private.Xml.Linq => 0x739bd4a8 => 86
	i32 1956758971, ; 315: System.Resources.Writer => 0x74a1c5bb => 99
	i32 1961813231, ; 316: Xamarin.AndroidX.Security.SecurityCrypto.dll => 0x74eee4ef => 279
	i32 1968388702, ; 317: Microsoft.Extensions.Configuration.dll => 0x75533a5e => 188
	i32 1983156543, ; 318: Xamarin.Kotlin.StdLib.Common.dll => 0x7634913f => 299
	i32 1985761444, ; 319: Xamarin.Android.Glide.GifDecoder => 0x765c50a4 => 224
	i32 2003115576, ; 320: el\Microsoft.Maui.Controls.resources => 0x77651e38 => 311
	i32 2011961780, ; 321: System.Buffers.dll => 0x77ec19b4 => 7
	i32 2014489277, ; 322: Microsoft.EntityFrameworkCore.Sqlite => 0x7812aabd => 185
	i32 2019465201, ; 323: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 264
	i32 2025202353, ; 324: ar/Microsoft.Maui.Controls.resources.dll => 0x78b622b1 => 306
	i32 2031763787, ; 325: Xamarin.Android.Glide => 0x791a414b => 221
	i32 2045470958, ; 326: System.Private.Xml => 0x79eb68ee => 87
	i32 2055257422, ; 327: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 259
	i32 2060060697, ; 328: System.Windows.dll => 0x7aca0819 => 151
	i32 2066184531, ; 329: de\Microsoft.Maui.Controls.resources => 0x7b277953 => 310
	i32 2070888862, ; 330: System.Diagnostics.TraceSource => 0x7b6f419e => 32
	i32 2079903147, ; 331: System.Runtime.dll => 0x7bf8cdab => 115
	i32 2090596640, ; 332: System.Numerics.Vectors => 0x7c9bf920 => 81
	i32 2103459038, ; 333: SQLitePCLRaw.provider.e_sqlite3.dll => 0x7d603cde => 211
	i32 2127167465, ; 334: System.Console => 0x7ec9ffe9 => 20
	i32 2142473426, ; 335: System.Collections.Specialized => 0x7fb38cd2 => 11
	i32 2143790110, ; 336: System.Xml.XmlSerializer.dll => 0x7fc7a41e => 159
	i32 2146852085, ; 337: Microsoft.VisualBasic.dll => 0x7ff65cf5 => 3
	i32 2159891885, ; 338: Microsoft.Maui => 0x80bd55ad => 201
	i32 2169148018, ; 339: hu\Microsoft.Maui.Controls.resources => 0x814a9272 => 318
	i32 2179924919, ; 340: FluentMigrator => 0x81ef03b7 => 171
	i32 2181898931, ; 341: Microsoft.Extensions.Options.dll => 0x820d22b3 => 196
	i32 2192057212, ; 342: Microsoft.Extensions.Logging.Abstractions => 0x82a8237c => 194
	i32 2193016926, ; 343: System.ObjectModel.dll => 0x82b6c85e => 83
	i32 2197979891, ; 344: Microsoft.Extensions.DependencyModel.dll => 0x830282f3 => 192
	i32 2201107256, ; 345: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 303
	i32 2201231467, ; 346: System.Net.Http => 0x8334206b => 63
	i32 2207618523, ; 347: it\Microsoft.Maui.Controls.resources => 0x839595db => 320
	i32 2217644978, ; 348: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x842e93b2 => 286
	i32 2222056684, ; 349: System.Threading.Tasks.Parallel => 0x8471e4ec => 140
	i32 2244775296, ; 350: Xamarin.AndroidX.LocalBroadcastManager => 0x85cc8d80 => 268
	i32 2252106437, ; 351: System.Xml.Serialization.dll => 0x863c6ac5 => 154
	i32 2252897993, ; 352: Microsoft.EntityFrameworkCore => 0x86487ec9 => 182
	i32 2256313426, ; 353: System.Globalization.Extensions => 0x867c9c52 => 40
	i32 2265110946, ; 354: System.Security.AccessControl.dll => 0x8702d9a2 => 116
	i32 2266799131, ; 355: Microsoft.Extensions.Configuration.Abstractions => 0x871c9c1b => 189
	i32 2267999099, ; 356: Xamarin.Android.Glide.DiskLruCache.dll => 0x872eeb7b => 223
	i32 2270573516, ; 357: fr/Microsoft.Maui.Controls.resources.dll => 0x875633cc => 314
	i32 2279755925, ; 358: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 275
	i32 2293034957, ; 359: System.ServiceModel.Web.dll => 0x88acefcd => 130
	i32 2295906218, ; 360: System.Net.Sockets => 0x88d8bfaa => 74
	i32 2298471582, ; 361: System.Net.Mail => 0x88ffe49e => 65
	i32 2303942373, ; 362: nb\Microsoft.Maui.Controls.resources => 0x89535ee5 => 324
	i32 2305521784, ; 363: System.Private.CoreLib.dll => 0x896b7878 => 169
	i32 2315684594, ; 364: Xamarin.AndroidX.Annotation.dll => 0x8a068af2 => 227
	i32 2320631194, ; 365: System.Threading.Tasks.Parallel.dll => 0x8a52059a => 140
	i32 2340441535, ; 366: System.Runtime.InteropServices.RuntimeInformation.dll => 0x8b804dbf => 105
	i32 2344264397, ; 367: System.ValueTuple => 0x8bbaa2cd => 148
	i32 2353062107, ; 368: System.Net.Primitives => 0x8c40e0db => 69
	i32 2368005991, ; 369: System.Xml.ReaderWriter.dll => 0x8d24e767 => 153
	i32 2371007202, ; 370: Microsoft.Extensions.Configuration => 0x8d52b2e2 => 188
	i32 2378619854, ; 371: System.Security.Cryptography.Csp.dll => 0x8dc6dbce => 120
	i32 2383496789, ; 372: System.Security.Principal.Windows.dll => 0x8e114655 => 126
	i32 2395872292, ; 373: id\Microsoft.Maui.Controls.resources => 0x8ece1c24 => 319
	i32 2401565422, ; 374: System.Web.HttpUtility => 0x8f24faee => 149
	i32 2403452196, ; 375: Xamarin.AndroidX.Emoji2.dll => 0x8f41c524 => 250
	i32 2409983638, ; 376: Microsoft.VisualStudio.DesignTools.MobileTapContracts.dll => 0x8fa56e96 => 341
	i32 2421380589, ; 377: System.Threading.Tasks.Dataflow => 0x905355ed => 138
	i32 2423080555, ; 378: Xamarin.AndroidX.Collection.Ktx.dll => 0x906d466b => 237
	i32 2427813419, ; 379: hi\Microsoft.Maui.Controls.resources => 0x90b57e2b => 316
	i32 2435356389, ; 380: System.Console.dll => 0x912896e5 => 20
	i32 2435904999, ; 381: System.ComponentModel.DataAnnotations.dll => 0x9130f5e7 => 14
	i32 2454642406, ; 382: System.Text.Encoding.dll => 0x924edee6 => 134
	i32 2458678730, ; 383: System.Net.Sockets.dll => 0x928c75ca => 74
	i32 2459001652, ; 384: System.Linq.Parallel.dll => 0x92916334 => 58
	i32 2465273461, ; 385: SQLitePCLRaw.batteries_v2.dll => 0x92f11675 => 208
	i32 2465532216, ; 386: Xamarin.AndroidX.ConstraintLayout.Core.dll => 0x92f50938 => 240
	i32 2471841756, ; 387: netstandard.dll => 0x93554fdc => 164
	i32 2475788418, ; 388: Java.Interop.dll => 0x93918882 => 165
	i32 2480646305, ; 389: Microsoft.Maui.Controls => 0x93dba8a1 => 199
	i32 2483903535, ; 390: System.ComponentModel.EventBasedAsync => 0x940d5c2f => 15
	i32 2484371297, ; 391: System.Net.ServicePoint => 0x94147f61 => 73
	i32 2486824558, ; 392: K4os.Hash.xxHash.dll => 0x9439ee6e => 180
	i32 2490993605, ; 393: System.AppContext.dll => 0x94798bc5 => 6
	i32 2498657740, ; 394: BouncyCastle.Cryptography.dll => 0x94ee7dcc => 170
	i32 2501346920, ; 395: System.Data.DataSetExtensions => 0x95178668 => 23
	i32 2505896520, ; 396: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x955cf248 => 262
	i32 2509217888, ; 397: System.Diagnostics.EventLog => 0x958fa060 => 214
	i32 2522472828, ; 398: Xamarin.Android.Glide.dll => 0x9659e17c => 221
	i32 2538310050, ; 399: System.Reflection.Emit.Lightweight.dll => 0x974b89a2 => 90
	i32 2550873716, ; 400: hr\Microsoft.Maui.Controls.resources => 0x980b3e74 => 317
	i32 2562349572, ; 401: Microsoft.CSharp => 0x98ba5a04 => 1
	i32 2570120770, ; 402: System.Text.Encodings.Web => 0x9930ee42 => 218
	i32 2581783588, ; 403: Xamarin.AndroidX.Lifecycle.Runtime.Ktx => 0x99e2e424 => 263
	i32 2581819634, ; 404: Xamarin.AndroidX.VectorDrawable.dll => 0x99e370f2 => 285
	i32 2585220780, ; 405: System.Text.Encoding.Extensions.dll => 0x9a1756ac => 133
	i32 2585805581, ; 406: System.Net.Ping => 0x9a20430d => 68
	i32 2589602615, ; 407: System.Threading.ThreadPool => 0x9a5a3337 => 143
	i32 2593496499, ; 408: pl\Microsoft.Maui.Controls.resources => 0x9a959db3 => 326
	i32 2605712449, ; 409: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 303
	i32 2611359322, ; 410: ZstdSharp.dll => 0x9ba62e5a => 304
	i32 2615233544, ; 411: Xamarin.AndroidX.Fragment.Ktx => 0x9be14c08 => 254
	i32 2616218305, ; 412: Microsoft.Extensions.Logging.Debug.dll => 0x9bf052c1 => 195
	i32 2617129537, ; 413: System.Private.Xml.dll => 0x9bfe3a41 => 87
	i32 2618712057, ; 414: System.Reflection.TypeExtensions.dll => 0x9c165ff9 => 95
	i32 2620871830, ; 415: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 244
	i32 2624644809, ; 416: Xamarin.AndroidX.DynamicAnimation => 0x9c70e6c9 => 249
	i32 2626831493, ; 417: ja\Microsoft.Maui.Controls.resources => 0x9c924485 => 321
	i32 2627185994, ; 418: System.Diagnostics.TextWriterTraceListener.dll => 0x9c97ad4a => 30
	i32 2629843544, ; 419: System.IO.Compression.ZipFile.dll => 0x9cc03a58 => 44
	i32 2633051222, ; 420: Xamarin.AndroidX.Lifecycle.LiveData => 0x9cf12c56 => 258
	i32 2634653062, ; 421: Microsoft.EntityFrameworkCore.Relational.dll => 0x9d099d86 => 184
	i32 2645932433, ; 422: FluentMigrator.Runner.Core => 0x9db5b991 => 174
	i32 2660759594, ; 423: System.Security.Cryptography.ProtectedData.dll => 0x9e97f82a => 216
	i32 2663391936, ; 424: Xamarin.Android.Glide.DiskLruCache => 0x9ec022c0 => 223
	i32 2663698177, ; 425: System.Runtime.Loader => 0x9ec4cf01 => 108
	i32 2664396074, ; 426: System.Xml.XDocument.dll => 0x9ecf752a => 155
	i32 2665622720, ; 427: System.Drawing.Primitives => 0x9ee22cc0 => 34
	i32 2676780864, ; 428: System.Data.Common.dll => 0x9f8c6f40 => 22
	i32 2686887180, ; 429: System.Runtime.Serialization.Xml.dll => 0xa026a50c => 113
	i32 2693849962, ; 430: System.IO.dll => 0xa090e36a => 56
	i32 2701096212, ; 431: Xamarin.AndroidX.Tracing.Tracing => 0xa0ff7514 => 283
	i32 2715334215, ; 432: System.Threading.Tasks.dll => 0xa1d8b647 => 141
	i32 2717744543, ; 433: System.Security.Claims => 0xa1fd7d9f => 117
	i32 2719963679, ; 434: System.Security.Cryptography.Cng.dll => 0xa21f5a1f => 119
	i32 2724373263, ; 435: System.Runtime.Numerics.dll => 0xa262a30f => 109
	i32 2732626843, ; 436: Xamarin.AndroidX.Activity => 0xa2e0939b => 225
	i32 2735172069, ; 437: System.Threading.Channels => 0xa30769e5 => 136
	i32 2737747696, ; 438: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 231
	i32 2740948882, ; 439: System.IO.Pipes.AccessControl => 0xa35f8f92 => 53
	i32 2748088231, ; 440: System.Runtime.InteropServices.JavaScript => 0xa3cc7fa7 => 104
	i32 2752995522, ; 441: pt-BR\Microsoft.Maui.Controls.resources => 0xa41760c2 => 327
	i32 2758225723, ; 442: Microsoft.Maui.Controls.Xaml => 0xa4672f3b => 200
	i32 2764765095, ; 443: Microsoft.Maui.dll => 0xa4caf7a7 => 201
	i32 2765824710, ; 444: System.Text.Encoding.CodePages.dll => 0xa4db22c6 => 132
	i32 2770495804, ; 445: Xamarin.Jetbrains.Annotations.dll => 0xa522693c => 297
	i32 2777249814, ; 446: AppInCube => 0xa5897816 => 0
	i32 2778768386, ; 447: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 288
	i32 2779977773, ; 448: Xamarin.AndroidX.ResourceInspection.Annotation.dll => 0xa5b3182d => 276
	i32 2785988530, ; 449: th\Microsoft.Maui.Controls.resources => 0xa60ecfb2 => 333
	i32 2788224221, ; 450: Xamarin.AndroidX.Fragment.Ktx.dll => 0xa630ecdd => 254
	i32 2801831435, ; 451: Microsoft.Maui.Graphics => 0xa7008e0b => 203
	i32 2803228030, ; 452: System.Xml.XPath.XDocument.dll => 0xa715dd7e => 156
	i32 2806116107, ; 453: es/Microsoft.Maui.Controls.resources.dll => 0xa741ef0b => 312
	i32 2810250172, ; 454: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 241
	i32 2819470561, ; 455: System.Xml.dll => 0xa80db4e1 => 160
	i32 2821205001, ; 456: System.ServiceProcess.dll => 0xa8282c09 => 131
	i32 2821294376, ; 457: Xamarin.AndroidX.ResourceInspection.Annotation => 0xa8298928 => 276
	i32 2824502124, ; 458: System.Xml.XmlDocument => 0xa85a7b6c => 158
	i32 2831556043, ; 459: nl/Microsoft.Maui.Controls.resources.dll => 0xa8c61dcb => 325
	i32 2833477479, ; 460: AppInCube.dll => 0xa8e36f67 => 0
	i32 2838993487, ; 461: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx.dll => 0xa9379a4f => 265
	i32 2841355853, ; 462: System.Security.Permissions => 0xa95ba64d => 217
	i32 2847789619, ; 463: Microsoft.EntityFrameworkCore.Relational => 0xa9bdd233 => 184
	i32 2849599387, ; 464: System.Threading.Overlapped.dll => 0xa9d96f9b => 137
	i32 2849763271, ; 465: de-DE\FluentMigrator.Abstractions.resources => 0xa9dbefc7 => 305
	i32 2853208004, ; 466: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 288
	i32 2855708567, ; 467: Xamarin.AndroidX.Transition => 0xaa36a797 => 284
	i32 2861098320, ; 468: Mono.Android.Export.dll => 0xaa88e550 => 166
	i32 2861189240, ; 469: Microsoft.Maui.Essentials => 0xaa8a4878 => 202
	i32 2867946736, ; 470: System.Security.Cryptography.ProtectedData => 0xaaf164f0 => 216
	i32 2870099610, ; 471: Xamarin.AndroidX.Activity.Ktx.dll => 0xab123e9a => 226
	i32 2875164099, ; 472: Jsr305Binding.dll => 0xab5f85c3 => 293
	i32 2875220617, ; 473: System.Globalization.Calendars.dll => 0xab606289 => 39
	i32 2884993177, ; 474: Xamarin.AndroidX.ExifInterface => 0xabf58099 => 252
	i32 2887636118, ; 475: System.Net.dll => 0xac1dd496 => 80
	i32 2899753641, ; 476: System.IO.UnmanagedMemoryStream => 0xacd6baa9 => 55
	i32 2900621748, ; 477: System.Dynamic.Runtime.dll => 0xace3f9b4 => 36
	i32 2901442782, ; 478: System.Reflection => 0xacf080de => 96
	i32 2905242038, ; 479: mscorlib.dll => 0xad2a79b6 => 163
	i32 2909740682, ; 480: System.Private.CoreLib => 0xad6f1e8a => 169
	i32 2916838712, ; 481: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 289
	i32 2919462931, ; 482: System.Numerics.Vectors.dll => 0xae037813 => 81
	i32 2921128767, ; 483: Xamarin.AndroidX.Annotation.Experimental.dll => 0xae1ce33f => 228
	i32 2936416060, ; 484: System.Resources.Reader => 0xaf06273c => 97
	i32 2940926066, ; 485: System.Diagnostics.StackTrace.dll => 0xaf4af872 => 29
	i32 2942453041, ; 486: System.Xml.XPath.XDocument => 0xaf624531 => 156
	i32 2944313911, ; 487: System.Configuration.ConfigurationManager.dll => 0xaf7eaa37 => 212
	i32 2959614098, ; 488: System.ComponentModel.dll => 0xb0682092 => 18
	i32 2968338931, ; 489: System.Security.Principal.Windows => 0xb0ed41f3 => 126
	i32 2972252294, ; 490: System.Security.Cryptography.Algorithms.dll => 0xb128f886 => 118
	i32 2978675010, ; 491: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 248
	i32 2987532451, ; 492: Xamarin.AndroidX.Security.SecurityCrypto => 0xb21220a3 => 279
	i32 2996846495, ; 493: Xamarin.AndroidX.Lifecycle.Process.dll => 0xb2a03f9f => 261
	i32 3012788804, ; 494: System.Configuration.ConfigurationManager => 0xb3938244 => 212
	i32 3016983068, ; 495: Xamarin.AndroidX.Startup.StartupRuntime => 0xb3d3821c => 281
	i32 3023353419, ; 496: WindowsBase.dll => 0xb434b64b => 162
	i32 3024354802, ; 497: Xamarin.AndroidX.Legacy.Support.Core.Utils => 0xb443fdf2 => 256
	i32 3025069135, ; 498: K4os.Compression.LZ4.Streams.dll => 0xb44ee44f => 179
	i32 3038032645, ; 499: _Microsoft.Android.Resource.Designer.dll => 0xb514b305 => 343
	i32 3056245963, ; 500: Xamarin.AndroidX.SavedState.SavedState.Ktx => 0xb62a9ccb => 278
	i32 3057625584, ; 501: Xamarin.AndroidX.Navigation.Common => 0xb63fa9f0 => 269
	i32 3059408633, ; 502: Mono.Android.Runtime => 0xb65adef9 => 167
	i32 3059793426, ; 503: System.ComponentModel.Primitives => 0xb660be12 => 16
	i32 3069363400, ; 504: Microsoft.Extensions.Caching.Abstractions.dll => 0xb6f2c4c8 => 186
	i32 3075834255, ; 505: System.Threading.Tasks => 0xb755818f => 141
	i32 3077302341, ; 506: hu/Microsoft.Maui.Controls.resources.dll => 0xb76be845 => 318
	i32 3085113210, ; 507: FluentMigrator.Runner.MySql.dll => 0xb7e3177a => 175
	i32 3089219899, ; 508: ZstdSharp => 0xb821c13b => 304
	i32 3090735792, ; 509: System.Security.Cryptography.X509Certificates.dll => 0xb838e2b0 => 124
	i32 3099732863, ; 510: System.Security.Claims.dll => 0xb8c22b7f => 117
	i32 3103600923, ; 511: System.Formats.Asn1 => 0xb8fd311b => 37
	i32 3111772706, ; 512: System.Runtime.Serialization => 0xb979e222 => 114
	i32 3121463068, ; 513: System.IO.FileSystem.AccessControl.dll => 0xba0dbf1c => 46
	i32 3124832203, ; 514: System.Threading.Tasks.Extensions => 0xba4127cb => 139
	i32 3132293585, ; 515: System.Security.AccessControl => 0xbab301d1 => 116
	i32 3147165239, ; 516: System.Diagnostics.Tracing.dll => 0xbb95ee37 => 33
	i32 3148237826, ; 517: GoogleGson.dll => 0xbba64c02 => 177
	i32 3159123045, ; 518: System.Reflection.Primitives.dll => 0xbc4c6465 => 94
	i32 3160747431, ; 519: System.IO.MemoryMappedFiles => 0xbc652da7 => 52
	i32 3170540552, ; 520: FluentMigrator.Runner.Core.dll => 0xbcfa9c08 => 174
	i32 3178803400, ; 521: Xamarin.AndroidX.Navigation.Fragment.dll => 0xbd78b0c8 => 270
	i32 3192346100, ; 522: System.Security.SecureString => 0xbe4755f4 => 128
	i32 3193515020, ; 523: System.Web => 0xbe592c0c => 150
	i32 3195844289, ; 524: Microsoft.Extensions.Caching.Abstractions => 0xbe7cb6c1 => 186
	i32 3204380047, ; 525: System.Data.dll => 0xbefef58f => 24
	i32 3209718065, ; 526: System.Xml.XmlDocument.dll => 0xbf506931 => 158
	i32 3211777861, ; 527: Xamarin.AndroidX.DocumentFile => 0xbf6fd745 => 247
	i32 3213246214, ; 528: System.Security.Permissions.dll => 0xbf863f06 => 217
	i32 3220365878, ; 529: System.Threading => 0xbff2e236 => 145
	i32 3226221578, ; 530: System.Runtime.Handles.dll => 0xc04c3c0a => 103
	i32 3251039220, ; 531: System.Reflection.DispatchProxy.dll => 0xc1c6ebf4 => 88
	i32 3258312781, ; 532: Xamarin.AndroidX.CardView => 0xc235e84d => 235
	i32 3265493905, ; 533: System.Linq.Queryable.dll => 0xc2a37b91 => 59
	i32 3265893370, ; 534: System.Threading.Tasks.Extensions.dll => 0xc2a993fa => 139
	i32 3277815716, ; 535: System.Resources.Writer.dll => 0xc35f7fa4 => 99
	i32 3279906254, ; 536: Microsoft.Win32.Registry.dll => 0xc37f65ce => 5
	i32 3280506390, ; 537: System.ComponentModel.Annotations.dll => 0xc3888e16 => 13
	i32 3286872994, ; 538: SQLite-net.dll => 0xc3e9b3a2 => 207
	i32 3290767353, ; 539: System.Security.Cryptography.Encoding => 0xc4251ff9 => 121
	i32 3299363146, ; 540: System.Text.Encoding => 0xc4a8494a => 134
	i32 3303498502, ; 541: System.Diagnostics.FileVersionInfo => 0xc4e76306 => 27
	i32 3305363605, ; 542: fi\Microsoft.Maui.Controls.resources => 0xc503d895 => 313
	i32 3316684772, ; 543: System.Net.Requests.dll => 0xc5b097e4 => 71
	i32 3317135071, ; 544: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 245
	i32 3317144872, ; 545: System.Data => 0xc5b79d28 => 24
	i32 3340431453, ; 546: Xamarin.AndroidX.Arch.Core.Runtime => 0xc71af05d => 233
	i32 3345895724, ; 547: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 0xc76e512c => 274
	i32 3346324047, ; 548: Xamarin.AndroidX.Navigation.Runtime => 0xc774da4f => 271
	i32 3357674450, ; 549: ru\Microsoft.Maui.Controls.resources => 0xc8220bd2 => 330
	i32 3358260929, ; 550: System.Text.Json => 0xc82afec1 => 219
	i32 3360279109, ; 551: SQLitePCLRaw.core => 0xc849ca45 => 209
	i32 3362336904, ; 552: Xamarin.AndroidX.Activity.Ktx => 0xc8693088 => 226
	i32 3362522851, ; 553: Xamarin.AndroidX.Core => 0xc86c06e3 => 242
	i32 3366347497, ; 554: Java.Interop => 0xc8a662e9 => 165
	i32 3374999561, ; 555: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 275
	i32 3381016424, ; 556: da\Microsoft.Maui.Controls.resources => 0xc9863768 => 309
	i32 3381033598, ; 557: K4os.Compression.LZ4 => 0xc9867a7e => 178
	i32 3395150330, ; 558: System.Runtime.CompilerServices.Unsafe.dll => 0xca5de1fa => 100
	i32 3403906625, ; 559: System.Security.Cryptography.OpenSsl.dll => 0xcae37e41 => 122
	i32 3405233483, ; 560: Xamarin.AndroidX.CustomView.PoolingContainer => 0xcaf7bd4b => 246
	i32 3428513518, ; 561: Microsoft.Extensions.DependencyInjection.dll => 0xcc5af6ee => 190
	i32 3429136800, ; 562: System.Xml => 0xcc6479a0 => 160
	i32 3430777524, ; 563: netstandard => 0xcc7d82b4 => 164
	i32 3441283291, ; 564: Xamarin.AndroidX.DynamicAnimation.dll => 0xcd1dd0db => 249
	i32 3445260447, ; 565: System.Formats.Tar => 0xcd5a809f => 38
	i32 3452344032, ; 566: Microsoft.Maui.Controls.Compatibility.dll => 0xcdc696e0 => 198
	i32 3463511458, ; 567: hr/Microsoft.Maui.Controls.resources.dll => 0xce70fda2 => 317
	i32 3467345667, ; 568: MySql.Data => 0xceab7f03 => 204
	i32 3471940407, ; 569: System.ComponentModel.TypeConverter.dll => 0xcef19b37 => 17
	i32 3476120550, ; 570: Mono.Android => 0xcf3163e6 => 168
	i32 3479583265, ; 571: ru/Microsoft.Maui.Controls.resources.dll => 0xcf663a21 => 330
	i32 3484440000, ; 572: ro\Microsoft.Maui.Controls.resources => 0xcfb055c0 => 329
	i32 3485117614, ; 573: System.Text.Json.dll => 0xcfbaacae => 219
	i32 3486566296, ; 574: System.Transactions => 0xcfd0c798 => 147
	i32 3493954962, ; 575: Xamarin.AndroidX.Concurrent.Futures.dll => 0xd0418592 => 238
	i32 3499097210, ; 576: Google.Protobuf.dll => 0xd08ffc7a => 176
	i32 3509114376, ; 577: System.Xml.Linq => 0xd128d608 => 152
	i32 3515174580, ; 578: System.Security.dll => 0xd1854eb4 => 129
	i32 3530912306, ; 579: System.Configuration => 0xd2757232 => 19
	i32 3539954161, ; 580: System.Net.HttpListener => 0xd2ff69f1 => 64
	i32 3560100363, ; 581: System.Threading.Timer => 0xd432d20b => 144
	i32 3570554715, ; 582: System.IO.FileSystem.AccessControl => 0xd4d2575b => 46
	i32 3580758918, ; 583: zh-HK\Microsoft.Maui.Controls.resources => 0xd56e0b86 => 337
	i32 3593904229, ; 584: FluentMigrator.Extensions.MySql.dll => 0xd636a065 => 173
	i32 3597029428, ; 585: Xamarin.Android.Glide.GifDecoder.dll => 0xd6665034 => 224
	i32 3598340787, ; 586: System.Net.WebSockets.Client => 0xd67a52b3 => 78
	i32 3605570793, ; 587: BouncyCastle.Cryptography => 0xd6e8a4e9 => 170
	i32 3608519521, ; 588: System.Linq.dll => 0xd715a361 => 60
	i32 3624195450, ; 589: System.Runtime.InteropServices.RuntimeInformation => 0xd804d57a => 105
	i32 3627220390, ; 590: Xamarin.AndroidX.Print.dll => 0xd832fda6 => 273
	i32 3633644679, ; 591: Xamarin.AndroidX.Annotation.Experimental => 0xd8950487 => 228
	i32 3638274909, ; 592: System.IO.FileSystem.Primitives.dll => 0xd8dbab5d => 48
	i32 3641597786, ; 593: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 259
	i32 3643446276, ; 594: tr\Microsoft.Maui.Controls.resources => 0xd92a9404 => 334
	i32 3643854240, ; 595: Xamarin.AndroidX.Navigation.Fragment => 0xd930cda0 => 270
	i32 3645089577, ; 596: System.ComponentModel.DataAnnotations => 0xd943a729 => 14
	i32 3645630983, ; 597: Google.Protobuf => 0xd94bea07 => 176
	i32 3657292374, ; 598: Microsoft.Extensions.Configuration.Abstractions.dll => 0xd9fdda56 => 189
	i32 3660523487, ; 599: System.Net.NetworkInformation => 0xda2f27df => 67
	i32 3672681054, ; 600: Mono.Android.dll => 0xdae8aa5e => 168
	i32 3676670898, ; 601: Microsoft.Maui.Controls.HotReload.Forms => 0xdb258bb2 => 340
	i32 3682565725, ; 602: Xamarin.AndroidX.Browser => 0xdb7f7e5d => 234
	i32 3684561358, ; 603: Xamarin.AndroidX.Concurrent.Futures => 0xdb9df1ce => 238
	i32 3697841164, ; 604: zh-Hant/Microsoft.Maui.Controls.resources.dll => 0xdc68940c => 339
	i32 3700866549, ; 605: System.Net.WebProxy.dll => 0xdc96bdf5 => 77
	i32 3706696989, ; 606: Xamarin.AndroidX.Core.Core.Ktx.dll => 0xdcefb51d => 243
	i32 3716563718, ; 607: System.Runtime.Intrinsics => 0xdd864306 => 107
	i32 3718780102, ; 608: Xamarin.AndroidX.Annotation => 0xdda814c6 => 227
	i32 3724971120, ; 609: Xamarin.AndroidX.Navigation.Common.dll => 0xde068c70 => 269
	i32 3732100267, ; 610: System.Net.NameResolution => 0xde7354ab => 66
	i32 3737834244, ; 611: System.Net.Http.Json.dll => 0xdecad304 => 62
	i32 3748608112, ; 612: System.Diagnostics.DiagnosticSource => 0xdf6f3870 => 213
	i32 3751444290, ; 613: System.Xml.XPath => 0xdf9a7f42 => 157
	i32 3754567612, ; 614: SQLitePCLRaw.provider.e_sqlite3 => 0xdfca27bc => 211
	i32 3786282454, ; 615: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 236
	i32 3792276235, ; 616: System.Collections.NonGeneric => 0xe2098b0b => 10
	i32 3800979733, ; 617: Microsoft.Maui.Controls.Compatibility => 0xe28e5915 => 198
	i32 3802395368, ; 618: System.Collections.Specialized.dll => 0xe2a3f2e8 => 11
	i32 3819260425, ; 619: System.Net.WebProxy => 0xe3a54a09 => 77
	i32 3823082795, ; 620: System.Security.Cryptography.dll => 0xe3df9d2b => 125
	i32 3829621856, ; 621: System.Numerics.dll => 0xe4436460 => 82
	i32 3841636137, ; 622: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xe4fab729 => 191
	i32 3844307129, ; 623: System.Net.Mail.dll => 0xe52378b9 => 65
	i32 3849253459, ; 624: System.Runtime.InteropServices.dll => 0xe56ef253 => 106
	i32 3870376305, ; 625: System.Net.HttpListener.dll => 0xe6b14171 => 64
	i32 3873536506, ; 626: System.Security.Principal => 0xe6e179fa => 127
	i32 3875112723, ; 627: System.Security.Cryptography.Encoding.dll => 0xe6f98713 => 121
	i32 3876362041, ; 628: SQLite-net => 0xe70c9739 => 207
	i32 3885497537, ; 629: System.Net.WebHeaderCollection.dll => 0xe797fcc1 => 76
	i32 3885922214, ; 630: Xamarin.AndroidX.Transition.dll => 0xe79e77a6 => 284
	i32 3888767677, ; 631: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 0xe7c9e2bd => 274
	i32 3889960447, ; 632: zh-Hans/Microsoft.Maui.Controls.resources.dll => 0xe7dc15ff => 338
	i32 3896106733, ; 633: System.Collections.Concurrent.dll => 0xe839deed => 8
	i32 3896760992, ; 634: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 242
	i32 3901907137, ; 635: Microsoft.VisualBasic.Core.dll => 0xe89260c1 => 2
	i32 3920810846, ; 636: System.IO.Compression.FileSystem.dll => 0xe9b2d35e => 43
	i32 3921031405, ; 637: Xamarin.AndroidX.VersionedParcelable.dll => 0xe9b630ed => 287
	i32 3928044579, ; 638: System.Xml.ReaderWriter => 0xea213423 => 153
	i32 3930554604, ; 639: System.Security.Principal.dll => 0xea4780ec => 127
	i32 3931092270, ; 640: Xamarin.AndroidX.Navigation.UI => 0xea4fb52e => 272
	i32 3945713374, ; 641: System.Data.DataSetExtensions.dll => 0xeb2ecede => 23
	i32 3953953790, ; 642: System.Text.Encoding.CodePages => 0xebac8bfe => 132
	i32 3955647286, ; 643: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 230
	i32 3959773229, ; 644: Xamarin.AndroidX.Lifecycle.Process => 0xec05582d => 261
	i32 3966452346, ; 645: FluentMigrator.dll => 0xec6b427a => 171
	i32 3980434154, ; 646: th/Microsoft.Maui.Controls.resources.dll => 0xed409aea => 333
	i32 3987592930, ; 647: he/Microsoft.Maui.Controls.resources.dll => 0xedadd6e2 => 315
	i32 4003436829, ; 648: System.Diagnostics.Process.dll => 0xee9f991d => 28
	i32 4015948917, ; 649: Xamarin.AndroidX.Annotation.Jvm.dll => 0xef5e8475 => 229
	i32 4023392905, ; 650: System.IO.Pipelines => 0xefd01a89 => 215
	i32 4025784931, ; 651: System.Memory => 0xeff49a63 => 61
	i32 4046471985, ; 652: Microsoft.Maui.Controls.Xaml.dll => 0xf1304331 => 200
	i32 4054681211, ; 653: System.Reflection.Emit.ILGeneration => 0xf1ad867b => 89
	i32 4068434129, ; 654: System.Private.Xml.Linq.dll => 0xf27f60d1 => 86
	i32 4073602200, ; 655: System.Threading.dll => 0xf2ce3c98 => 145
	i32 4079385022, ; 656: MySqlConnector.dll => 0xf32679be => 205
	i32 4094352644, ; 657: Microsoft.Maui.Essentials.dll => 0xf40add04 => 202
	i32 4099507663, ; 658: System.Drawing.dll => 0xf45985cf => 35
	i32 4100113165, ; 659: System.Private.Uri => 0xf462c30d => 85
	i32 4101593132, ; 660: Xamarin.AndroidX.Emoji2 => 0xf479582c => 250
	i32 4101842092, ; 661: Microsoft.Extensions.Caching.Memory => 0xf47d24ac => 187
	i32 4102112229, ; 662: pt/Microsoft.Maui.Controls.resources.dll => 0xf48143e5 => 328
	i32 4125707920, ; 663: ms/Microsoft.Maui.Controls.resources.dll => 0xf5e94e90 => 323
	i32 4126470640, ; 664: Microsoft.Extensions.DependencyInjection => 0xf5f4f1f0 => 190
	i32 4127667938, ; 665: System.IO.FileSystem.Watcher => 0xf60736e2 => 49
	i32 4130442656, ; 666: System.AppContext => 0xf6318da0 => 6
	i32 4135660637, ; 667: FluentMigrator.Extensions.MySql => 0xf6812c5d => 173
	i32 4147896353, ; 668: System.Reflection.Emit.ILGeneration.dll => 0xf73be021 => 89
	i32 4150914736, ; 669: uk\Microsoft.Maui.Controls.resources => 0xf769eeb0 => 335
	i32 4151237749, ; 670: System.Core => 0xf76edc75 => 21
	i32 4159265925, ; 671: System.Xml.XmlSerializer => 0xf7e95c85 => 159
	i32 4161255271, ; 672: System.Reflection.TypeExtensions => 0xf807b767 => 95
	i32 4164802419, ; 673: System.IO.FileSystem.Watcher.dll => 0xf83dd773 => 49
	i32 4167813518, ; 674: FluentMigrator.Abstractions => 0xf86bc98e => 172
	i32 4181436372, ; 675: System.Runtime.Serialization.Primitives => 0xf93ba7d4 => 112
	i32 4182413190, ; 676: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 266
	i32 4182880526, ; 677: Microsoft.VisualStudio.DesignTools.MobileTapContracts => 0xf951b10e => 341
	i32 4185676441, ; 678: System.Security => 0xf97c5a99 => 129
	i32 4196529839, ; 679: System.Net.WebClient.dll => 0xfa21f6af => 75
	i32 4213026141, ; 680: System.Diagnostics.DiagnosticSource.dll => 0xfb1dad5d => 213
	i32 4256097574, ; 681: Xamarin.AndroidX.Core.Core.Ktx => 0xfdaee526 => 243
	i32 4258378803, ; 682: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx => 0xfdd1b433 => 265
	i32 4260525087, ; 683: System.Buffers => 0xfdf2741f => 7
	i32 4271975918, ; 684: Microsoft.Maui.Controls.dll => 0xfea12dee => 199
	i32 4274976490, ; 685: System.Runtime.Numerics => 0xfecef6ea => 109
	i32 4292120959, ; 686: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 266
	i32 4294763496 ; 687: Xamarin.AndroidX.ExifInterface.dll => 0xfffce3e8 => 252
], align 4

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [688 x i32] [
	i32 67, ; 0
	i32 66, ; 1
	i32 107, ; 2
	i32 192, ; 3
	i32 262, ; 4
	i32 296, ; 5
	i32 47, ; 6
	i32 206, ; 7
	i32 79, ; 8
	i32 142, ; 9
	i32 29, ; 10
	i32 339, ; 11
	i32 123, ; 12
	i32 203, ; 13
	i32 101, ; 14
	i32 280, ; 15
	i32 106, ; 16
	i32 280, ; 17
	i32 136, ; 18
	i32 300, ; 19
	i32 76, ; 20
	i32 123, ; 21
	i32 13, ; 22
	i32 236, ; 23
	i32 131, ; 24
	i32 282, ; 25
	i32 148, ; 26
	i32 336, ; 27
	i32 337, ; 28
	i32 18, ; 29
	i32 234, ; 30
	i32 26, ; 31
	i32 256, ; 32
	i32 1, ; 33
	i32 58, ; 34
	i32 41, ; 35
	i32 90, ; 36
	i32 239, ; 37
	i32 144, ; 38
	i32 258, ; 39
	i32 255, ; 40
	i32 308, ; 41
	i32 53, ; 42
	i32 68, ; 43
	i32 336, ; 44
	i32 225, ; 45
	i32 82, ; 46
	i32 321, ; 47
	i32 257, ; 48
	i32 210, ; 49
	i32 320, ; 50
	i32 130, ; 51
	i32 54, ; 52
	i32 146, ; 53
	i32 73, ; 54
	i32 142, ; 55
	i32 61, ; 56
	i32 143, ; 57
	i32 343, ; 58
	i32 162, ; 59
	i32 332, ; 60
	i32 240, ; 61
	i32 12, ; 62
	i32 253, ; 63
	i32 124, ; 64
	i32 149, ; 65
	i32 112, ; 66
	i32 163, ; 67
	i32 161, ; 68
	i32 255, ; 69
	i32 268, ; 70
	i32 83, ; 71
	i32 319, ; 72
	i32 313, ; 73
	i32 197, ; 74
	i32 147, ; 75
	i32 300, ; 76
	i32 59, ; 77
	i32 193, ; 78
	i32 50, ; 79
	i32 102, ; 80
	i32 113, ; 81
	i32 39, ; 82
	i32 293, ; 83
	i32 291, ; 84
	i32 119, ; 85
	i32 327, ; 86
	i32 51, ; 87
	i32 43, ; 88
	i32 118, ; 89
	i32 179, ; 90
	i32 245, ; 91
	i32 325, ; 92
	i32 251, ; 93
	i32 80, ; 94
	i32 218, ; 95
	i32 287, ; 96
	i32 232, ; 97
	i32 8, ; 98
	i32 72, ; 99
	i32 307, ; 100
	i32 152, ; 101
	i32 302, ; 102
	i32 151, ; 103
	i32 91, ; 104
	i32 297, ; 105
	i32 44, ; 106
	i32 322, ; 107
	i32 310, ; 108
	i32 301, ; 109
	i32 108, ; 110
	i32 128, ; 111
	i32 305, ; 112
	i32 208, ; 113
	i32 25, ; 114
	i32 222, ; 115
	i32 71, ; 116
	i32 54, ; 117
	i32 45, ; 118
	i32 331, ; 119
	i32 196, ; 120
	i32 246, ; 121
	i32 22, ; 122
	i32 260, ; 123
	i32 85, ; 124
	i32 42, ; 125
	i32 157, ; 126
	i32 70, ; 127
	i32 273, ; 128
	i32 3, ; 129
	i32 41, ; 130
	i32 62, ; 131
	i32 16, ; 132
	i32 52, ; 133
	i32 334, ; 134
	i32 296, ; 135
	i32 104, ; 136
	i32 206, ; 137
	i32 301, ; 138
	i32 294, ; 139
	i32 257, ; 140
	i32 33, ; 141
	i32 155, ; 142
	i32 204, ; 143
	i32 84, ; 144
	i32 31, ; 145
	i32 12, ; 146
	i32 50, ; 147
	i32 55, ; 148
	i32 277, ; 149
	i32 35, ; 150
	i32 191, ; 151
	i32 309, ; 152
	i32 295, ; 153
	i32 230, ; 154
	i32 34, ; 155
	i32 57, ; 156
	i32 264, ; 157
	i32 177, ; 158
	i32 17, ; 159
	i32 298, ; 160
	i32 214, ; 161
	i32 161, ; 162
	i32 322, ; 163
	i32 263, ; 164
	i32 195, ; 165
	i32 220, ; 166
	i32 290, ; 167
	i32 183, ; 168
	i32 172, ; 169
	i32 328, ; 170
	i32 150, ; 171
	i32 286, ; 172
	i32 271, ; 173
	i32 183, ; 174
	i32 326, ; 175
	i32 232, ; 176
	i32 187, ; 177
	i32 28, ; 178
	i32 51, ; 179
	i32 324, ; 180
	i32 291, ; 181
	i32 5, ; 182
	i32 308, ; 183
	i32 281, ; 184
	i32 285, ; 185
	i32 237, ; 186
	i32 302, ; 187
	i32 229, ; 188
	i32 209, ; 189
	i32 248, ; 190
	i32 84, ; 191
	i32 290, ; 192
	i32 60, ; 193
	i32 111, ; 194
	i32 56, ; 195
	i32 338, ; 196
	i32 277, ; 197
	i32 98, ; 198
	i32 19, ; 199
	i32 241, ; 200
	i32 110, ; 201
	i32 100, ; 202
	i32 101, ; 203
	i32 306, ; 204
	i32 103, ; 205
	i32 294, ; 206
	i32 70, ; 207
	i32 37, ; 208
	i32 31, ; 209
	i32 102, ; 210
	i32 72, ; 211
	i32 312, ; 212
	i32 9, ; 213
	i32 122, ; 214
	i32 45, ; 215
	i32 231, ; 216
	i32 197, ; 217
	i32 9, ; 218
	i32 42, ; 219
	i32 4, ; 220
	i32 180, ; 221
	i32 278, ; 222
	i32 181, ; 223
	i32 316, ; 224
	i32 205, ; 225
	i32 311, ; 226
	i32 30, ; 227
	i32 135, ; 228
	i32 91, ; 229
	i32 92, ; 230
	i32 331, ; 231
	i32 48, ; 232
	i32 138, ; 233
	i32 111, ; 234
	i32 137, ; 235
	i32 247, ; 236
	i32 114, ; 237
	i32 295, ; 238
	i32 154, ; 239
	i32 340, ; 240
	i32 75, ; 241
	i32 78, ; 242
	i32 267, ; 243
	i32 36, ; 244
	i32 289, ; 245
	i32 251, ; 246
	i32 244, ; 247
	i32 63, ; 248
	i32 135, ; 249
	i32 15, ; 250
	i32 115, ; 251
	i32 283, ; 252
	i32 292, ; 253
	i32 239, ; 254
	i32 47, ; 255
	i32 69, ; 256
	i32 79, ; 257
	i32 125, ; 258
	i32 181, ; 259
	i32 182, ; 260
	i32 93, ; 261
	i32 120, ; 262
	i32 299, ; 263
	i32 26, ; 264
	i32 210, ; 265
	i32 260, ; 266
	i32 96, ; 267
	i32 27, ; 268
	i32 235, ; 269
	i32 329, ; 270
	i32 307, ; 271
	i32 146, ; 272
	i32 215, ; 273
	i32 166, ; 274
	i32 4, ; 275
	i32 97, ; 276
	i32 32, ; 277
	i32 92, ; 278
	i32 282, ; 279
	i32 193, ; 280
	i32 21, ; 281
	i32 40, ; 282
	i32 167, ; 283
	i32 323, ; 284
	i32 253, ; 285
	i32 315, ; 286
	i32 267, ; 287
	i32 298, ; 288
	i32 292, ; 289
	i32 272, ; 290
	i32 2, ; 291
	i32 133, ; 292
	i32 110, ; 293
	i32 342, ; 294
	i32 194, ; 295
	i32 220, ; 296
	i32 335, ; 297
	i32 222, ; 298
	i32 332, ; 299
	i32 57, ; 300
	i32 94, ; 301
	i32 314, ; 302
	i32 38, ; 303
	i32 233, ; 304
	i32 342, ; 305
	i32 185, ; 306
	i32 25, ; 307
	i32 93, ; 308
	i32 175, ; 309
	i32 88, ; 310
	i32 98, ; 311
	i32 10, ; 312
	i32 178, ; 313
	i32 86, ; 314
	i32 99, ; 315
	i32 279, ; 316
	i32 188, ; 317
	i32 299, ; 318
	i32 224, ; 319
	i32 311, ; 320
	i32 7, ; 321
	i32 185, ; 322
	i32 264, ; 323
	i32 306, ; 324
	i32 221, ; 325
	i32 87, ; 326
	i32 259, ; 327
	i32 151, ; 328
	i32 310, ; 329
	i32 32, ; 330
	i32 115, ; 331
	i32 81, ; 332
	i32 211, ; 333
	i32 20, ; 334
	i32 11, ; 335
	i32 159, ; 336
	i32 3, ; 337
	i32 201, ; 338
	i32 318, ; 339
	i32 171, ; 340
	i32 196, ; 341
	i32 194, ; 342
	i32 83, ; 343
	i32 192, ; 344
	i32 303, ; 345
	i32 63, ; 346
	i32 320, ; 347
	i32 286, ; 348
	i32 140, ; 349
	i32 268, ; 350
	i32 154, ; 351
	i32 182, ; 352
	i32 40, ; 353
	i32 116, ; 354
	i32 189, ; 355
	i32 223, ; 356
	i32 314, ; 357
	i32 275, ; 358
	i32 130, ; 359
	i32 74, ; 360
	i32 65, ; 361
	i32 324, ; 362
	i32 169, ; 363
	i32 227, ; 364
	i32 140, ; 365
	i32 105, ; 366
	i32 148, ; 367
	i32 69, ; 368
	i32 153, ; 369
	i32 188, ; 370
	i32 120, ; 371
	i32 126, ; 372
	i32 319, ; 373
	i32 149, ; 374
	i32 250, ; 375
	i32 341, ; 376
	i32 138, ; 377
	i32 237, ; 378
	i32 316, ; 379
	i32 20, ; 380
	i32 14, ; 381
	i32 134, ; 382
	i32 74, ; 383
	i32 58, ; 384
	i32 208, ; 385
	i32 240, ; 386
	i32 164, ; 387
	i32 165, ; 388
	i32 199, ; 389
	i32 15, ; 390
	i32 73, ; 391
	i32 180, ; 392
	i32 6, ; 393
	i32 170, ; 394
	i32 23, ; 395
	i32 262, ; 396
	i32 214, ; 397
	i32 221, ; 398
	i32 90, ; 399
	i32 317, ; 400
	i32 1, ; 401
	i32 218, ; 402
	i32 263, ; 403
	i32 285, ; 404
	i32 133, ; 405
	i32 68, ; 406
	i32 143, ; 407
	i32 326, ; 408
	i32 303, ; 409
	i32 304, ; 410
	i32 254, ; 411
	i32 195, ; 412
	i32 87, ; 413
	i32 95, ; 414
	i32 244, ; 415
	i32 249, ; 416
	i32 321, ; 417
	i32 30, ; 418
	i32 44, ; 419
	i32 258, ; 420
	i32 184, ; 421
	i32 174, ; 422
	i32 216, ; 423
	i32 223, ; 424
	i32 108, ; 425
	i32 155, ; 426
	i32 34, ; 427
	i32 22, ; 428
	i32 113, ; 429
	i32 56, ; 430
	i32 283, ; 431
	i32 141, ; 432
	i32 117, ; 433
	i32 119, ; 434
	i32 109, ; 435
	i32 225, ; 436
	i32 136, ; 437
	i32 231, ; 438
	i32 53, ; 439
	i32 104, ; 440
	i32 327, ; 441
	i32 200, ; 442
	i32 201, ; 443
	i32 132, ; 444
	i32 297, ; 445
	i32 0, ; 446
	i32 288, ; 447
	i32 276, ; 448
	i32 333, ; 449
	i32 254, ; 450
	i32 203, ; 451
	i32 156, ; 452
	i32 312, ; 453
	i32 241, ; 454
	i32 160, ; 455
	i32 131, ; 456
	i32 276, ; 457
	i32 158, ; 458
	i32 325, ; 459
	i32 0, ; 460
	i32 265, ; 461
	i32 217, ; 462
	i32 184, ; 463
	i32 137, ; 464
	i32 305, ; 465
	i32 288, ; 466
	i32 284, ; 467
	i32 166, ; 468
	i32 202, ; 469
	i32 216, ; 470
	i32 226, ; 471
	i32 293, ; 472
	i32 39, ; 473
	i32 252, ; 474
	i32 80, ; 475
	i32 55, ; 476
	i32 36, ; 477
	i32 96, ; 478
	i32 163, ; 479
	i32 169, ; 480
	i32 289, ; 481
	i32 81, ; 482
	i32 228, ; 483
	i32 97, ; 484
	i32 29, ; 485
	i32 156, ; 486
	i32 212, ; 487
	i32 18, ; 488
	i32 126, ; 489
	i32 118, ; 490
	i32 248, ; 491
	i32 279, ; 492
	i32 261, ; 493
	i32 212, ; 494
	i32 281, ; 495
	i32 162, ; 496
	i32 256, ; 497
	i32 179, ; 498
	i32 343, ; 499
	i32 278, ; 500
	i32 269, ; 501
	i32 167, ; 502
	i32 16, ; 503
	i32 186, ; 504
	i32 141, ; 505
	i32 318, ; 506
	i32 175, ; 507
	i32 304, ; 508
	i32 124, ; 509
	i32 117, ; 510
	i32 37, ; 511
	i32 114, ; 512
	i32 46, ; 513
	i32 139, ; 514
	i32 116, ; 515
	i32 33, ; 516
	i32 177, ; 517
	i32 94, ; 518
	i32 52, ; 519
	i32 174, ; 520
	i32 270, ; 521
	i32 128, ; 522
	i32 150, ; 523
	i32 186, ; 524
	i32 24, ; 525
	i32 158, ; 526
	i32 247, ; 527
	i32 217, ; 528
	i32 145, ; 529
	i32 103, ; 530
	i32 88, ; 531
	i32 235, ; 532
	i32 59, ; 533
	i32 139, ; 534
	i32 99, ; 535
	i32 5, ; 536
	i32 13, ; 537
	i32 207, ; 538
	i32 121, ; 539
	i32 134, ; 540
	i32 27, ; 541
	i32 313, ; 542
	i32 71, ; 543
	i32 245, ; 544
	i32 24, ; 545
	i32 233, ; 546
	i32 274, ; 547
	i32 271, ; 548
	i32 330, ; 549
	i32 219, ; 550
	i32 209, ; 551
	i32 226, ; 552
	i32 242, ; 553
	i32 165, ; 554
	i32 275, ; 555
	i32 309, ; 556
	i32 178, ; 557
	i32 100, ; 558
	i32 122, ; 559
	i32 246, ; 560
	i32 190, ; 561
	i32 160, ; 562
	i32 164, ; 563
	i32 249, ; 564
	i32 38, ; 565
	i32 198, ; 566
	i32 317, ; 567
	i32 204, ; 568
	i32 17, ; 569
	i32 168, ; 570
	i32 330, ; 571
	i32 329, ; 572
	i32 219, ; 573
	i32 147, ; 574
	i32 238, ; 575
	i32 176, ; 576
	i32 152, ; 577
	i32 129, ; 578
	i32 19, ; 579
	i32 64, ; 580
	i32 144, ; 581
	i32 46, ; 582
	i32 337, ; 583
	i32 173, ; 584
	i32 224, ; 585
	i32 78, ; 586
	i32 170, ; 587
	i32 60, ; 588
	i32 105, ; 589
	i32 273, ; 590
	i32 228, ; 591
	i32 48, ; 592
	i32 259, ; 593
	i32 334, ; 594
	i32 270, ; 595
	i32 14, ; 596
	i32 176, ; 597
	i32 189, ; 598
	i32 67, ; 599
	i32 168, ; 600
	i32 340, ; 601
	i32 234, ; 602
	i32 238, ; 603
	i32 339, ; 604
	i32 77, ; 605
	i32 243, ; 606
	i32 107, ; 607
	i32 227, ; 608
	i32 269, ; 609
	i32 66, ; 610
	i32 62, ; 611
	i32 213, ; 612
	i32 157, ; 613
	i32 211, ; 614
	i32 236, ; 615
	i32 10, ; 616
	i32 198, ; 617
	i32 11, ; 618
	i32 77, ; 619
	i32 125, ; 620
	i32 82, ; 621
	i32 191, ; 622
	i32 65, ; 623
	i32 106, ; 624
	i32 64, ; 625
	i32 127, ; 626
	i32 121, ; 627
	i32 207, ; 628
	i32 76, ; 629
	i32 284, ; 630
	i32 274, ; 631
	i32 338, ; 632
	i32 8, ; 633
	i32 242, ; 634
	i32 2, ; 635
	i32 43, ; 636
	i32 287, ; 637
	i32 153, ; 638
	i32 127, ; 639
	i32 272, ; 640
	i32 23, ; 641
	i32 132, ; 642
	i32 230, ; 643
	i32 261, ; 644
	i32 171, ; 645
	i32 333, ; 646
	i32 315, ; 647
	i32 28, ; 648
	i32 229, ; 649
	i32 215, ; 650
	i32 61, ; 651
	i32 200, ; 652
	i32 89, ; 653
	i32 86, ; 654
	i32 145, ; 655
	i32 205, ; 656
	i32 202, ; 657
	i32 35, ; 658
	i32 85, ; 659
	i32 250, ; 660
	i32 187, ; 661
	i32 328, ; 662
	i32 323, ; 663
	i32 190, ; 664
	i32 49, ; 665
	i32 6, ; 666
	i32 173, ; 667
	i32 89, ; 668
	i32 335, ; 669
	i32 21, ; 670
	i32 159, ; 671
	i32 95, ; 672
	i32 49, ; 673
	i32 172, ; 674
	i32 112, ; 675
	i32 266, ; 676
	i32 341, ; 677
	i32 129, ; 678
	i32 75, ; 679
	i32 213, ; 680
	i32 243, ; 681
	i32 265, ; 682
	i32 7, ; 683
	i32 199, ; 684
	i32 109, ; 685
	i32 266, ; 686
	i32 252 ; 687
], align 4

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 4

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 4

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 4

; Functions

; Function attributes: "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" uwtable willreturn
define void @xamarin_app_init(ptr nocapture noundef readnone %env, ptr noundef %fn) local_unnamed_addr #0
{
	%fnIsNull = icmp eq ptr %fn, null
	br i1 %fnIsNull, label %1, label %2

1: ; preds = %0
	%putsResult = call noundef i32 @puts(ptr @.str.0)
	call void @abort()
	unreachable 

2: ; preds = %1, %0
	store ptr %fn, ptr @get_function_pointer, align 4, !tbaa !3
	ret void
}

; Strings
@.str.0 = private unnamed_addr constant [40 x i8] c"get_function_pointer MUST be specified\0A\00", align 1

;MarshalMethodName
@.MarshalMethodName.0_name = private unnamed_addr constant [1 x i8] c"\00", align 1

; External functions

; Function attributes: noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8"
declare void @abort() local_unnamed_addr #2

; Function attributes: nofree nounwind
declare noundef i32 @puts(ptr noundef) local_unnamed_addr #1
attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "stackrealign" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "stackrealign" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" }

; Metadata
!llvm.module.flags = !{!0, !1, !7}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!"Xamarin.Android remotes/origin/release/8.0.4xx @ a8cd27e430e55df3e3c1e3a43d35c11d9512a2db"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
!7 = !{i32 1, !"NumRegisterParameters", i32 0}
