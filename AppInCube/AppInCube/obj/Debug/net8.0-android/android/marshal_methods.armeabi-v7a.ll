; ModuleID = 'marshal_methods.armeabi-v7a.ll'
source_filename = "marshal_methods.armeabi-v7a.ll"
target datalayout = "e-m:e-p:32:32-Fi8-i64:64-v128:64:128-a:0:32-n32-S64"
target triple = "armv7-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [353 x ptr] zeroinitializer, align 4

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [700 x i32] [
	i32 2616222, ; 0: System.Net.NetworkInformation.dll => 0x27eb9e => 67
	i32 10166715, ; 1: System.Net.NameResolution.dll => 0x9b21bb => 66
	i32 15721112, ; 2: System.Runtime.Intrinsics.dll => 0xefe298 => 107
	i32 26230656, ; 3: Microsoft.Extensions.DependencyModel => 0x1903f80 => 192
	i32 32687329, ; 4: Xamarin.AndroidX.Lifecycle.Runtime => 0x1f2c4e1 => 264
	i32 34715100, ; 5: Xamarin.Google.Guava.ListenableFuture.dll => 0x211b5dc => 298
	i32 34839235, ; 6: System.IO.FileSystem.DriveInfo => 0x2139ac3 => 47
	i32 39109920, ; 7: Newtonsoft.Json.dll => 0x254c520 => 207
	i32 39485524, ; 8: System.Net.WebSockets.dll => 0x25a8054 => 79
	i32 42639949, ; 9: System.Threading.Thread => 0x28aa24d => 142
	i32 66541672, ; 10: System.Diagnostics.StackTrace => 0x3f75868 => 29
	i32 67008169, ; 11: zh-Hant\Microsoft.Maui.Controls.resources => 0x3fe76a9 => 345
	i32 68219467, ; 12: System.Security.Cryptography.Primitives => 0x410f24b => 123
	i32 72070932, ; 13: Microsoft.Maui.Graphics.dll => 0x44bb714 => 203
	i32 82292897, ; 14: System.Runtime.CompilerServices.VisualC.dll => 0x4e7b0a1 => 101
	i32 101534019, ; 15: Xamarin.AndroidX.SlidingPaneLayout => 0x60d4943 => 282
	i32 117431740, ; 16: System.Runtime.InteropServices => 0x6ffddbc => 106
	i32 120558881, ; 17: Xamarin.AndroidX.SlidingPaneLayout.dll => 0x72f9521 => 282
	i32 122350210, ; 18: System.Threading.Channels.dll => 0x74aea82 => 136
	i32 134690465, ; 19: Xamarin.Kotlin.StdLib.Jdk7.dll => 0x80736a1 => 306
	i32 142721839, ; 20: System.Net.WebHeaderCollection => 0x881c32f => 76
	i32 149972175, ; 21: System.Security.Cryptography.Primitives.dll => 0x8f064cf => 123
	i32 159306688, ; 22: System.ComponentModel.Annotations => 0x97ed3c0 => 13
	i32 165246403, ; 23: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 238
	i32 176265551, ; 24: System.ServiceProcess => 0xa81994f => 131
	i32 182336117, ; 25: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 284
	i32 184328833, ; 26: System.ValueTuple.dll => 0xafca281 => 148
	i32 195452805, ; 27: vi/Microsoft.Maui.Controls.resources.dll => 0xba65f85 => 342
	i32 199333315, ; 28: zh-HK/Microsoft.Maui.Controls.resources.dll => 0xbe195c3 => 343
	i32 205061960, ; 29: System.ComponentModel => 0xc38ff48 => 18
	i32 209399409, ; 30: Xamarin.AndroidX.Browser.dll => 0xc7b2e71 => 236
	i32 220171995, ; 31: System.Diagnostics.Debug => 0xd1f8edb => 26
	i32 230216969, ; 32: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 0xdb8d509 => 258
	i32 230752869, ; 33: Microsoft.CSharp.dll => 0xdc10265 => 1
	i32 231409092, ; 34: System.Linq.Parallel => 0xdcb05c4 => 58
	i32 231814094, ; 35: System.Globalization => 0xdd133ce => 41
	i32 246610117, ; 36: System.Reflection.Emit.Lightweight => 0xeb2f8c5 => 90
	i32 261689757, ; 37: Xamarin.AndroidX.ConstraintLayout.dll => 0xf99119d => 241
	i32 276479776, ; 38: System.Threading.Timer.dll => 0x107abf20 => 144
	i32 278686392, ; 39: Xamarin.AndroidX.Lifecycle.LiveData.dll => 0x109c6ab8 => 260
	i32 280482487, ; 40: Xamarin.AndroidX.Interpolator => 0x10b7d2b7 => 257
	i32 280992041, ; 41: cs/Microsoft.Maui.Controls.resources.dll => 0x10bf9929 => 314
	i32 291076382, ; 42: System.IO.Pipes.AccessControl.dll => 0x1159791e => 53
	i32 298918909, ; 43: System.Net.Ping.dll => 0x11d123fd => 68
	i32 317674968, ; 44: vi\Microsoft.Maui.Controls.resources => 0x12ef55d8 => 342
	i32 318968648, ; 45: Xamarin.AndroidX.Activity.dll => 0x13031348 => 227
	i32 321597661, ; 46: System.Numerics => 0x132b30dd => 82
	i32 336156722, ; 47: ja/Microsoft.Maui.Controls.resources.dll => 0x14095832 => 327
	i32 342366114, ; 48: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 259
	i32 347068432, ; 49: SQLitePCLRaw.lib.e_sqlite3.android.dll => 0x14afd810 => 212
	i32 356389973, ; 50: it/Microsoft.Maui.Controls.resources.dll => 0x153e1455 => 326
	i32 360082299, ; 51: System.ServiceModel.Web => 0x15766b7b => 130
	i32 367780167, ; 52: System.IO.Pipes => 0x15ebe147 => 54
	i32 374914964, ; 53: System.Transactions.Local => 0x1658bf94 => 146
	i32 375677976, ; 54: System.Net.ServicePoint.dll => 0x16646418 => 73
	i32 379916513, ; 55: System.Threading.Thread.dll => 0x16a510e1 => 142
	i32 385762202, ; 56: System.Memory.dll => 0x16fe439a => 61
	i32 392610295, ; 57: System.Threading.ThreadPool.dll => 0x1766c1f7 => 143
	i32 395744057, ; 58: _Microsoft.Android.Resource.Designer => 0x17969339 => 349
	i32 403441872, ; 59: WindowsBase => 0x180c08d0 => 162
	i32 435591531, ; 60: sv/Microsoft.Maui.Controls.resources.dll => 0x19f6996b => 338
	i32 440834030, ; 61: Microsoft.Toolkit.Uwp.Notifications => 0x1a4697ee => 204
	i32 441335492, ; 62: Xamarin.AndroidX.ConstraintLayout.Core => 0x1a4e3ec4 => 242
	i32 442565967, ; 63: System.Collections => 0x1a61054f => 12
	i32 450948140, ; 64: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 255
	i32 451504562, ; 65: System.Security.Cryptography.X509Certificates => 0x1ae969b2 => 124
	i32 456227837, ; 66: System.Web.HttpUtility.dll => 0x1b317bfd => 149
	i32 459347974, ; 67: System.Runtime.Serialization.Primitives.dll => 0x1b611806 => 112
	i32 465846621, ; 68: mscorlib => 0x1bc4415d => 163
	i32 469710990, ; 69: System.dll => 0x1bff388e => 161
	i32 476646585, ; 70: Xamarin.AndroidX.Interpolator.dll => 0x1c690cb9 => 257
	i32 486930444, ; 71: Xamarin.AndroidX.LocalBroadcastManager.dll => 0x1d05f80c => 270
	i32 498788369, ; 72: System.ObjectModel => 0x1dbae811 => 83
	i32 500358224, ; 73: id/Microsoft.Maui.Controls.resources.dll => 0x1dd2dc50 => 325
	i32 503918385, ; 74: fi/Microsoft.Maui.Controls.resources.dll => 0x1e092f31 => 319
	i32 504143952, ; 75: Plugin.LocalNotification.dll => 0x1e0ca050 => 208
	i32 513247710, ; 76: Microsoft.Extensions.Primitives.dll => 0x1e9789de => 197
	i32 526420162, ; 77: System.Transactions.dll => 0x1f6088c2 => 147
	i32 527452488, ; 78: Xamarin.Kotlin.StdLib.Jdk7 => 0x1f704948 => 306
	i32 530272170, ; 79: System.Linq.Queryable => 0x1f9b4faa => 59
	i32 539058512, ; 80: Microsoft.Extensions.Logging => 0x20216150 => 193
	i32 540030774, ; 81: System.IO.FileSystem.dll => 0x20303736 => 50
	i32 545304856, ; 82: System.Runtime.Extensions => 0x2080b118 => 102
	i32 546455878, ; 83: System.Runtime.Serialization.Xml => 0x20924146 => 113
	i32 549171840, ; 84: System.Globalization.Calendars => 0x20bbb280 => 39
	i32 557405415, ; 85: Jsr305Binding => 0x213954e7 => 295
	i32 569601784, ; 86: Xamarin.AndroidX.Window.Extensions.Core.Core => 0x21f36ef8 => 293
	i32 577335427, ; 87: System.Security.Cryptography.Cng => 0x22697083 => 119
	i32 592146354, ; 88: pt-BR/Microsoft.Maui.Controls.resources.dll => 0x234b6fb2 => 333
	i32 601371474, ; 89: System.IO.IsolatedStorage.dll => 0x23d83352 => 51
	i32 605376203, ; 90: System.IO.Compression.FileSystem => 0x24154ecb => 43
	i32 613668793, ; 91: System.Security.Cryptography.Algorithms => 0x2493d7b9 => 118
	i32 618636221, ; 92: K4os.Compression.LZ4.Streams => 0x24dfa3bd => 179
	i32 627609679, ; 93: Xamarin.AndroidX.CustomView => 0x2568904f => 247
	i32 627931235, ; 94: nl\Microsoft.Maui.Controls.resources => 0x256d7863 => 331
	i32 639843206, ; 95: Xamarin.AndroidX.Emoji2.ViewsHelper.dll => 0x26233b86 => 253
	i32 643868501, ; 96: System.Net => 0x2660a755 => 80
	i32 662205335, ; 97: System.Text.Encodings.Web.dll => 0x27787397 => 220
	i32 663517072, ; 98: Xamarin.AndroidX.VersionedParcelable => 0x278c7790 => 289
	i32 666292255, ; 99: Xamarin.AndroidX.Arch.Core.Common.dll => 0x27b6d01f => 234
	i32 672442732, ; 100: System.Collections.Concurrent => 0x2814a96c => 8
	i32 683518922, ; 101: System.Net.Security => 0x28bdabca => 72
	i32 688181140, ; 102: ca/Microsoft.Maui.Controls.resources.dll => 0x2904cf94 => 313
	i32 690569205, ; 103: System.Xml.Linq.dll => 0x29293ff5 => 152
	i32 691348768, ; 104: Xamarin.KotlinX.Coroutines.Android.dll => 0x29352520 => 308
	i32 693804605, ; 105: System.Windows => 0x295a9e3d => 151
	i32 699345723, ; 106: System.Reflection.Emit => 0x29af2b3b => 91
	i32 700284507, ; 107: Xamarin.Jetbrains.Annotations => 0x29bd7e5b => 303
	i32 700358131, ; 108: System.IO.Compression.ZipFile => 0x29be9df3 => 44
	i32 706645707, ; 109: ko/Microsoft.Maui.Controls.resources.dll => 0x2a1e8ecb => 328
	i32 709557578, ; 110: de/Microsoft.Maui.Controls.resources.dll => 0x2a4afd4a => 316
	i32 720511267, ; 111: Xamarin.Kotlin.StdLib.Jdk8 => 0x2af22123 => 307
	i32 722857257, ; 112: System.Runtime.Loader.dll => 0x2b15ed29 => 108
	i32 735137430, ; 113: System.Security.SecureString.dll => 0x2bd14e96 => 128
	i32 748085224, ; 114: de-DE/FluentMigrator.Abstractions.resources.dll => 0x2c96dfe8 => 311
	i32 748832960, ; 115: SQLitePCLRaw.batteries_v2 => 0x2ca248c0 => 210
	i32 752232764, ; 116: System.Diagnostics.Contracts.dll => 0x2cd6293c => 25
	i32 755313932, ; 117: Xamarin.Android.Glide.Annotations.dll => 0x2d052d0c => 224
	i32 759454413, ; 118: System.Net.Requests => 0x2d445acd => 71
	i32 762598435, ; 119: System.IO.Pipes.dll => 0x2d745423 => 54
	i32 775507847, ; 120: System.IO.Compression => 0x2e394f87 => 45
	i32 777317022, ; 121: sk\Microsoft.Maui.Controls.resources => 0x2e54ea9e => 337
	i32 789151979, ; 122: Microsoft.Extensions.Options => 0x2f0980eb => 196
	i32 790371945, ; 123: Xamarin.AndroidX.CustomView.PoolingContainer.dll => 0x2f1c1e69 => 248
	i32 804715423, ; 124: System.Data.Common => 0x2ff6fb9f => 22
	i32 807930345, ; 125: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx.dll => 0x302809e9 => 262
	i32 823281589, ; 126: System.Private.Uri.dll => 0x311247b5 => 85
	i32 830298997, ; 127: System.IO.Compression.Brotli => 0x317d5b75 => 42
	i32 832635846, ; 128: System.Xml.XPath.dll => 0x31a103c6 => 157
	i32 834051424, ; 129: System.Net.Quic => 0x31b69d60 => 70
	i32 843511501, ; 130: Xamarin.AndroidX.Print => 0x3246f6cd => 275
	i32 873119928, ; 131: Microsoft.VisualBasic => 0x340ac0b8 => 3
	i32 877678880, ; 132: System.Globalization.dll => 0x34505120 => 41
	i32 878954865, ; 133: System.Net.Http.Json => 0x3463c971 => 62
	i32 904024072, ; 134: System.ComponentModel.Primitives.dll => 0x35e25008 => 16
	i32 911108515, ; 135: System.IO.MemoryMappedFiles.dll => 0x364e69a3 => 52
	i32 926902833, ; 136: tr/Microsoft.Maui.Controls.resources.dll => 0x373f6a31 => 340
	i32 928116545, ; 137: Xamarin.Google.Guava.ListenableFuture => 0x3751ef41 => 298
	i32 952186615, ; 138: System.Runtime.InteropServices.JavaScript.dll => 0x38c136f7 => 104
	i32 955402788, ; 139: Newtonsoft.Json => 0x38f24a24 => 207
	i32 956575887, ; 140: Xamarin.Kotlin.StdLib.Jdk8.dll => 0x3904308f => 307
	i32 966729478, ; 141: Xamarin.Google.Crypto.Tink.Android => 0x399f1f06 => 296
	i32 967690846, ; 142: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 259
	i32 975236339, ; 143: System.Diagnostics.Tracing => 0x3a20ecf3 => 33
	i32 975874589, ; 144: System.Xml.XDocument => 0x3a2aaa1d => 155
	i32 983077409, ; 145: MySql.Data.dll => 0x3a989221 => 205
	i32 986514023, ; 146: System.Private.DataContractSerialization.dll => 0x3acd0267 => 84
	i32 987214855, ; 147: System.Diagnostics.Tools => 0x3ad7b407 => 31
	i32 992768348, ; 148: System.Collections.dll => 0x3b2c715c => 12
	i32 994442037, ; 149: System.IO.FileSystem => 0x3b45fb35 => 50
	i32 1001831731, ; 150: System.IO.UnmanagedMemoryStream.dll => 0x3bb6bd33 => 55
	i32 1012816738, ; 151: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 279
	i32 1019214401, ; 152: System.Drawing => 0x3cbffa41 => 35
	i32 1028951442, ; 153: Microsoft.Extensions.DependencyInjection.Abstractions => 0x3d548d92 => 191
	i32 1029334545, ; 154: da/Microsoft.Maui.Controls.resources.dll => 0x3d5a6611 => 315
	i32 1031528504, ; 155: Xamarin.Google.ErrorProne.Annotations.dll => 0x3d7be038 => 297
	i32 1035644815, ; 156: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 232
	i32 1036536393, ; 157: System.Drawing.Primitives.dll => 0x3dc84a49 => 34
	i32 1044663988, ; 158: System.Linq.Expressions.dll => 0x3e444eb4 => 57
	i32 1052210849, ; 159: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 266
	i32 1067306892, ; 160: GoogleGson => 0x3f9dcf8c => 177
	i32 1082857460, ; 161: System.ComponentModel.TypeConverter => 0x408b17f4 => 17
	i32 1084122840, ; 162: Xamarin.Kotlin.StdLib => 0x409e66d8 => 304
	i32 1089913930, ; 163: System.Diagnostics.EventLog.dll => 0x40f6c44a => 216
	i32 1098259244, ; 164: System => 0x41761b2c => 161
	i32 1118262833, ; 165: ko\Microsoft.Maui.Controls.resources => 0x42a75631 => 328
	i32 1121599056, ; 166: Xamarin.AndroidX.Lifecycle.Runtime.Ktx.dll => 0x42da3e50 => 265
	i32 1127624469, ; 167: Microsoft.Extensions.Logging.Debug => 0x43362f15 => 195
	i32 1145483052, ; 168: System.Windows.Extensions.dll => 0x4446af2c => 222
	i32 1149092582, ; 169: Xamarin.AndroidX.Window => 0x447dc2e6 => 292
	i32 1157931901, ; 170: Microsoft.EntityFrameworkCore.Abstractions => 0x4504a37d => 183
	i32 1157939332, ; 171: FluentMigrator.Abstractions.dll => 0x4504c084 => 172
	i32 1168523401, ; 172: pt\Microsoft.Maui.Controls.resources => 0x45a64089 => 334
	i32 1170634674, ; 173: System.Web.dll => 0x45c677b2 => 150
	i32 1175144683, ; 174: Xamarin.AndroidX.VectorDrawable.Animated => 0x460b48eb => 288
	i32 1178241025, ; 175: Xamarin.AndroidX.Navigation.Runtime.dll => 0x463a8801 => 273
	i32 1202000627, ; 176: Microsoft.EntityFrameworkCore.Abstractions.dll => 0x47a512f3 => 183
	i32 1203215381, ; 177: pl/Microsoft.Maui.Controls.resources.dll => 0x47b79c15 => 332
	i32 1204270330, ; 178: Xamarin.AndroidX.Arch.Core.Common => 0x47c7b4fa => 234
	i32 1204575371, ; 179: Microsoft.Extensions.Caching.Memory.dll => 0x47cc5c8b => 187
	i32 1208641965, ; 180: System.Diagnostics.Process => 0x480a69ad => 28
	i32 1219128291, ; 181: System.IO.IsolatedStorage => 0x48aa6be3 => 51
	i32 1234928153, ; 182: nb/Microsoft.Maui.Controls.resources.dll => 0x499b8219 => 330
	i32 1243150071, ; 183: Xamarin.AndroidX.Window.Extensions.Core.Core.dll => 0x4a18f6f7 => 293
	i32 1253011324, ; 184: Microsoft.Win32.Registry => 0x4aaf6f7c => 5
	i32 1260983243, ; 185: cs\Microsoft.Maui.Controls.resources => 0x4b2913cb => 314
	i32 1264511973, ; 186: Xamarin.AndroidX.Startup.StartupRuntime.dll => 0x4b5eebe5 => 283
	i32 1267360935, ; 187: Xamarin.AndroidX.VectorDrawable => 0x4b8a64a7 => 287
	i32 1273260888, ; 188: Xamarin.AndroidX.Collection.Ktx => 0x4be46b58 => 239
	i32 1275534314, ; 189: Xamarin.KotlinX.Coroutines.Android => 0x4c071bea => 308
	i32 1278448581, ; 190: Xamarin.AndroidX.Annotation.Jvm => 0x4c3393c5 => 231
	i32 1292207520, ; 191: SQLitePCLRaw.core.dll => 0x4d0585a0 => 211
	i32 1293217323, ; 192: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 250
	i32 1309188875, ; 193: System.Private.DataContractSerialization => 0x4e08a30b => 84
	i32 1322716291, ; 194: Xamarin.AndroidX.Window.dll => 0x4ed70c83 => 292
	i32 1324164729, ; 195: System.Linq => 0x4eed2679 => 60
	i32 1335329327, ; 196: System.Runtime.Serialization.Json.dll => 0x4f97822f => 111
	i32 1364015309, ; 197: System.IO => 0x514d38cd => 56
	i32 1373134921, ; 198: zh-Hans\Microsoft.Maui.Controls.resources => 0x51d86049 => 344
	i32 1376866003, ; 199: Xamarin.AndroidX.SavedState => 0x52114ed3 => 279
	i32 1379779777, ; 200: System.Resources.ResourceManager => 0x523dc4c1 => 98
	i32 1402170036, ; 201: System.Configuration.dll => 0x53936ab4 => 19
	i32 1406073936, ; 202: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 243
	i32 1408764838, ; 203: System.Runtime.Serialization.Formatters.dll => 0x53f80ba6 => 110
	i32 1411638395, ; 204: System.Runtime.CompilerServices.Unsafe => 0x5423e47b => 100
	i32 1422545099, ; 205: System.Runtime.CompilerServices.VisualC => 0x54ca50cb => 101
	i32 1430672901, ; 206: ar\Microsoft.Maui.Controls.resources => 0x55465605 => 312
	i32 1434145427, ; 207: System.Runtime.Handles => 0x557b5293 => 103
	i32 1435222561, ; 208: Xamarin.Google.Crypto.Tink.Android.dll => 0x558bc221 => 296
	i32 1439761251, ; 209: System.Net.Quic.dll => 0x55d10363 => 70
	i32 1452070440, ; 210: System.Formats.Asn1.dll => 0x568cd628 => 37
	i32 1453312822, ; 211: System.Diagnostics.Tools.dll => 0x569fcb36 => 31
	i32 1457743152, ; 212: System.Runtime.Extensions.dll => 0x56e36530 => 102
	i32 1458022317, ; 213: System.Net.Security.dll => 0x56e7a7ad => 72
	i32 1461004990, ; 214: es\Microsoft.Maui.Controls.resources => 0x57152abe => 318
	i32 1461234159, ; 215: System.Collections.Immutable.dll => 0x5718a9ef => 9
	i32 1461719063, ; 216: System.Security.Cryptography.OpenSsl => 0x57201017 => 122
	i32 1462112819, ; 217: System.IO.Compression.dll => 0x57261233 => 45
	i32 1469204771, ; 218: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 233
	i32 1470490898, ; 219: Microsoft.Extensions.Primitives => 0x57a5e912 => 197
	i32 1479771757, ; 220: System.Collections.Immutable => 0x5833866d => 9
	i32 1480492111, ; 221: System.IO.Compression.Brotli.dll => 0x583e844f => 42
	i32 1487239319, ; 222: Microsoft.Win32.Primitives => 0x58a57897 => 4
	i32 1487250139, ; 223: K4os.Hash.xxHash => 0x58a5a2db => 180
	i32 1490025113, ; 224: Xamarin.AndroidX.SavedState.SavedState.Ktx.dll => 0x58cffa99 => 280
	i32 1490351284, ; 225: Microsoft.Data.Sqlite.dll => 0x58d4f4b4 => 181
	i32 1493001747, ; 226: hi/Microsoft.Maui.Controls.resources.dll => 0x58fd6613 => 322
	i32 1511525525, ; 227: MySqlConnector => 0x5a180c95 => 206
	i32 1514721132, ; 228: el/Microsoft.Maui.Controls.resources.dll => 0x5a48cf6c => 317
	i32 1524747670, ; 229: Plugin.LocalNotification => 0x5ae1cd96 => 208
	i32 1536373174, ; 230: System.Diagnostics.TextWriterTraceListener => 0x5b9331b6 => 30
	i32 1543031311, ; 231: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 135
	i32 1543355203, ; 232: System.Reflection.Emit.dll => 0x5bfdbb43 => 91
	i32 1550322496, ; 233: System.Reflection.Extensions.dll => 0x5c680b40 => 92
	i32 1551623176, ; 234: sk/Microsoft.Maui.Controls.resources.dll => 0x5c7be408 => 337
	i32 1565862583, ; 235: System.IO.FileSystem.Primitives => 0x5d552ab7 => 48
	i32 1566207040, ; 236: System.Threading.Tasks.Dataflow.dll => 0x5d5a6c40 => 138
	i32 1573704789, ; 237: System.Runtime.Serialization.Json => 0x5dccd455 => 111
	i32 1580037396, ; 238: System.Threading.Overlapped => 0x5e2d7514 => 137
	i32 1582372066, ; 239: Xamarin.AndroidX.DocumentFile.dll => 0x5e5114e2 => 249
	i32 1592978981, ; 240: System.Runtime.Serialization.dll => 0x5ef2ee25 => 114
	i32 1597949149, ; 241: Xamarin.Google.ErrorProne.Annotations => 0x5f3ec4dd => 297
	i32 1601112923, ; 242: System.Xml.Serialization => 0x5f6f0b5b => 154
	i32 1603525486, ; 243: Microsoft.Maui.Controls.HotReload.Forms.dll => 0x5f93db6e => 346
	i32 1604827217, ; 244: System.Net.WebClient => 0x5fa7b851 => 75
	i32 1618516317, ; 245: System.Net.WebSockets.Client.dll => 0x6078995d => 78
	i32 1622152042, ; 246: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 269
	i32 1622358360, ; 247: System.Dynamic.Runtime => 0x60b33958 => 36
	i32 1624863272, ; 248: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 291
	i32 1635184631, ; 249: Xamarin.AndroidX.Emoji2.ViewsHelper => 0x6176eff7 => 253
	i32 1636350590, ; 250: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 246
	i32 1639515021, ; 251: System.Net.Http.dll => 0x61b9038d => 63
	i32 1639986890, ; 252: System.Text.RegularExpressions => 0x61c036ca => 135
	i32 1641389582, ; 253: System.ComponentModel.EventBasedAsync.dll => 0x61d59e0e => 15
	i32 1657153582, ; 254: System.Runtime => 0x62c6282e => 115
	i32 1658241508, ; 255: Xamarin.AndroidX.Tracing.Tracing.dll => 0x62d6c1e4 => 285
	i32 1658251792, ; 256: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 294
	i32 1670060433, ; 257: Xamarin.AndroidX.ConstraintLayout => 0x638b1991 => 241
	i32 1675553242, ; 258: System.IO.FileSystem.DriveInfo.dll => 0x63dee9da => 47
	i32 1677501392, ; 259: System.Net.Primitives.dll => 0x63fca3d0 => 69
	i32 1678508291, ; 260: System.Net.WebSockets => 0x640c0103 => 79
	i32 1679769178, ; 261: System.Security.Cryptography => 0x641f3e5a => 125
	i32 1688112883, ; 262: Microsoft.Data.Sqlite => 0x649e8ef3 => 181
	i32 1689493916, ; 263: Microsoft.EntityFrameworkCore.dll => 0x64b3a19c => 182
	i32 1691477237, ; 264: System.Reflection.Metadata => 0x64d1e4f5 => 93
	i32 1696967625, ; 265: System.Security.Cryptography.Csp => 0x6525abc9 => 120
	i32 1698840827, ; 266: Xamarin.Kotlin.StdLib.Common => 0x654240fb => 305
	i32 1701541528, ; 267: System.Diagnostics.Debug.dll => 0x656b7698 => 26
	i32 1711441057, ; 268: SQLitePCLRaw.lib.e_sqlite3.android => 0x660284a1 => 212
	i32 1720223769, ; 269: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx => 0x66888819 => 262
	i32 1726116996, ; 270: System.Reflection.dll => 0x66e27484 => 96
	i32 1728033016, ; 271: System.Diagnostics.FileVersionInfo.dll => 0x66ffb0f8 => 27
	i32 1729485958, ; 272: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 237
	i32 1736233607, ; 273: ro/Microsoft.Maui.Controls.resources.dll => 0x677cd287 => 335
	i32 1743415430, ; 274: ca\Microsoft.Maui.Controls.resources => 0x67ea6886 => 313
	i32 1744735666, ; 275: System.Transactions.Local.dll => 0x67fe8db2 => 146
	i32 1746115085, ; 276: System.IO.Pipelines.dll => 0x68139a0d => 217
	i32 1746316138, ; 277: Mono.Android.Export => 0x6816ab6a => 166
	i32 1750313021, ; 278: Microsoft.Win32.Primitives.dll => 0x6853a83d => 4
	i32 1758240030, ; 279: System.Resources.Reader.dll => 0x68cc9d1e => 97
	i32 1763938596, ; 280: System.Diagnostics.TraceSource.dll => 0x69239124 => 32
	i32 1765942094, ; 281: System.Reflection.Extensions => 0x6942234e => 92
	i32 1766324549, ; 282: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 284
	i32 1770582343, ; 283: Microsoft.Extensions.Logging.dll => 0x6988f147 => 193
	i32 1776026572, ; 284: System.Core.dll => 0x69dc03cc => 21
	i32 1777075843, ; 285: System.Globalization.Extensions.dll => 0x69ec0683 => 40
	i32 1780572499, ; 286: Mono.Android.Runtime.dll => 0x6a216153 => 167
	i32 1782862114, ; 287: ms\Microsoft.Maui.Controls.resources => 0x6a445122 => 329
	i32 1788241197, ; 288: Xamarin.AndroidX.Fragment => 0x6a96652d => 255
	i32 1793755602, ; 289: he\Microsoft.Maui.Controls.resources => 0x6aea89d2 => 321
	i32 1808609942, ; 290: Xamarin.AndroidX.Loader => 0x6bcd3296 => 269
	i32 1813058853, ; 291: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 304
	i32 1813201214, ; 292: Xamarin.Google.Android.Material => 0x6c13413e => 294
	i32 1818569960, ; 293: Xamarin.AndroidX.Navigation.UI.dll => 0x6c652ce8 => 274
	i32 1818787751, ; 294: Microsoft.VisualBasic.Core => 0x6c687fa7 => 2
	i32 1824175904, ; 295: System.Text.Encoding.Extensions => 0x6cbab720 => 133
	i32 1824722060, ; 296: System.Runtime.Serialization.Formatters => 0x6cc30c8c => 110
	i32 1827303595, ; 297: Microsoft.VisualStudio.DesignTools.TapContract => 0x6cea70ab => 348
	i32 1828688058, ; 298: Microsoft.Extensions.Logging.Abstractions.dll => 0x6cff90ba => 194
	i32 1829150748, ; 299: System.Windows.Extensions => 0x6d06a01c => 222
	i32 1842015223, ; 300: uk/Microsoft.Maui.Controls.resources.dll => 0x6dcaebf7 => 341
	i32 1847515442, ; 301: Xamarin.Android.Glide.Annotations => 0x6e1ed932 => 224
	i32 1853025655, ; 302: sv\Microsoft.Maui.Controls.resources => 0x6e72ed77 => 338
	i32 1858542181, ; 303: System.Linq.Expressions => 0x6ec71a65 => 57
	i32 1870277092, ; 304: System.Reflection.Primitives => 0x6f7a29e4 => 94
	i32 1875935024, ; 305: fr\Microsoft.Maui.Controls.resources => 0x6fd07f30 => 320
	i32 1879696579, ; 306: System.Formats.Tar.dll => 0x7009e4c3 => 38
	i32 1885316902, ; 307: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0x705fa726 => 235
	i32 1885918049, ; 308: Microsoft.VisualStudio.DesignTools.TapContract.dll => 0x7068d361 => 348
	i32 1886040351, ; 309: Microsoft.EntityFrameworkCore.Sqlite.dll => 0x706ab11f => 185
	i32 1888955245, ; 310: System.Diagnostics.Contracts => 0x70972b6d => 25
	i32 1889954781, ; 311: System.Reflection.Metadata.dll => 0x70a66bdd => 93
	i32 1897940508, ; 312: FluentMigrator.Runner.MySql => 0x7120461c => 175
	i32 1898237753, ; 313: System.Reflection.DispatchProxy => 0x7124cf39 => 88
	i32 1900610850, ; 314: System.Resources.ResourceManager.dll => 0x71490522 => 98
	i32 1908813208, ; 315: Xamarin.GooglePlayServices.Basement => 0x71c62d98 => 300
	i32 1910275211, ; 316: System.Collections.NonGeneric.dll => 0x71dc7c8b => 10
	i32 1925302748, ; 317: K4os.Compression.LZ4.dll => 0x72c1c9dc => 178
	i32 1939592360, ; 318: System.Private.Xml.Linq => 0x739bd4a8 => 86
	i32 1956758971, ; 319: System.Resources.Writer => 0x74a1c5bb => 99
	i32 1961813231, ; 320: Xamarin.AndroidX.Security.SecurityCrypto.dll => 0x74eee4ef => 281
	i32 1968388702, ; 321: Microsoft.Extensions.Configuration.dll => 0x75533a5e => 188
	i32 1983156543, ; 322: Xamarin.Kotlin.StdLib.Common.dll => 0x7634913f => 305
	i32 1985761444, ; 323: Xamarin.Android.Glide.GifDecoder => 0x765c50a4 => 226
	i32 2003115576, ; 324: el\Microsoft.Maui.Controls.resources => 0x77651e38 => 317
	i32 2011961780, ; 325: System.Buffers.dll => 0x77ec19b4 => 7
	i32 2014489277, ; 326: Microsoft.EntityFrameworkCore.Sqlite => 0x7812aabd => 185
	i32 2019465201, ; 327: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 266
	i32 2025202353, ; 328: ar/Microsoft.Maui.Controls.resources.dll => 0x78b622b1 => 312
	i32 2031763787, ; 329: Xamarin.Android.Glide => 0x791a414b => 223
	i32 2045470958, ; 330: System.Private.Xml => 0x79eb68ee => 87
	i32 2055257422, ; 331: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 261
	i32 2060060697, ; 332: System.Windows.dll => 0x7aca0819 => 151
	i32 2066184531, ; 333: de\Microsoft.Maui.Controls.resources => 0x7b277953 => 316
	i32 2070888862, ; 334: System.Diagnostics.TraceSource => 0x7b6f419e => 32
	i32 2079903147, ; 335: System.Runtime.dll => 0x7bf8cdab => 115
	i32 2090596640, ; 336: System.Numerics.Vectors => 0x7c9bf920 => 81
	i32 2103459038, ; 337: SQLitePCLRaw.provider.e_sqlite3.dll => 0x7d603cde => 213
	i32 2127167465, ; 338: System.Console => 0x7ec9ffe9 => 20
	i32 2129483829, ; 339: Xamarin.GooglePlayServices.Base.dll => 0x7eed5835 => 299
	i32 2142473426, ; 340: System.Collections.Specialized => 0x7fb38cd2 => 11
	i32 2143790110, ; 341: System.Xml.XmlSerializer.dll => 0x7fc7a41e => 159
	i32 2146852085, ; 342: Microsoft.VisualBasic.dll => 0x7ff65cf5 => 3
	i32 2159891885, ; 343: Microsoft.Maui => 0x80bd55ad => 201
	i32 2169148018, ; 344: hu\Microsoft.Maui.Controls.resources => 0x814a9272 => 324
	i32 2179924919, ; 345: FluentMigrator => 0x81ef03b7 => 171
	i32 2181898931, ; 346: Microsoft.Extensions.Options.dll => 0x820d22b3 => 196
	i32 2192057212, ; 347: Microsoft.Extensions.Logging.Abstractions => 0x82a8237c => 194
	i32 2193016926, ; 348: System.ObjectModel.dll => 0x82b6c85e => 83
	i32 2197979891, ; 349: Microsoft.Extensions.DependencyModel.dll => 0x830282f3 => 192
	i32 2201107256, ; 350: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 309
	i32 2201231467, ; 351: System.Net.Http => 0x8334206b => 63
	i32 2207618523, ; 352: it\Microsoft.Maui.Controls.resources => 0x839595db => 326
	i32 2217644978, ; 353: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x842e93b2 => 288
	i32 2222056684, ; 354: System.Threading.Tasks.Parallel => 0x8471e4ec => 140
	i32 2244775296, ; 355: Xamarin.AndroidX.LocalBroadcastManager => 0x85cc8d80 => 270
	i32 2252106437, ; 356: System.Xml.Serialization.dll => 0x863c6ac5 => 154
	i32 2252897993, ; 357: Microsoft.EntityFrameworkCore => 0x86487ec9 => 182
	i32 2256313426, ; 358: System.Globalization.Extensions => 0x867c9c52 => 40
	i32 2265110946, ; 359: System.Security.AccessControl.dll => 0x8702d9a2 => 116
	i32 2266799131, ; 360: Microsoft.Extensions.Configuration.Abstractions => 0x871c9c1b => 189
	i32 2267999099, ; 361: Xamarin.Android.Glide.DiskLruCache.dll => 0x872eeb7b => 225
	i32 2270573516, ; 362: fr/Microsoft.Maui.Controls.resources.dll => 0x875633cc => 320
	i32 2279755925, ; 363: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 277
	i32 2293034957, ; 364: System.ServiceModel.Web.dll => 0x88acefcd => 130
	i32 2295906218, ; 365: System.Net.Sockets => 0x88d8bfaa => 74
	i32 2298471582, ; 366: System.Net.Mail => 0x88ffe49e => 65
	i32 2303942373, ; 367: nb\Microsoft.Maui.Controls.resources => 0x89535ee5 => 330
	i32 2305521784, ; 368: System.Private.CoreLib.dll => 0x896b7878 => 169
	i32 2315684594, ; 369: Xamarin.AndroidX.Annotation.dll => 0x8a068af2 => 229
	i32 2320631194, ; 370: System.Threading.Tasks.Parallel.dll => 0x8a52059a => 140
	i32 2340441535, ; 371: System.Runtime.InteropServices.RuntimeInformation.dll => 0x8b804dbf => 105
	i32 2344264397, ; 372: System.ValueTuple => 0x8bbaa2cd => 148
	i32 2353062107, ; 373: System.Net.Primitives => 0x8c40e0db => 69
	i32 2368005991, ; 374: System.Xml.ReaderWriter.dll => 0x8d24e767 => 153
	i32 2371007202, ; 375: Microsoft.Extensions.Configuration => 0x8d52b2e2 => 188
	i32 2378619854, ; 376: System.Security.Cryptography.Csp.dll => 0x8dc6dbce => 120
	i32 2383496789, ; 377: System.Security.Principal.Windows.dll => 0x8e114655 => 126
	i32 2395872292, ; 378: id\Microsoft.Maui.Controls.resources => 0x8ece1c24 => 325
	i32 2401565422, ; 379: System.Web.HttpUtility => 0x8f24faee => 149
	i32 2403452196, ; 380: Xamarin.AndroidX.Emoji2.dll => 0x8f41c524 => 252
	i32 2409983638, ; 381: Microsoft.VisualStudio.DesignTools.MobileTapContracts.dll => 0x8fa56e96 => 347
	i32 2421380589, ; 382: System.Threading.Tasks.Dataflow => 0x905355ed => 138
	i32 2423080555, ; 383: Xamarin.AndroidX.Collection.Ktx.dll => 0x906d466b => 239
	i32 2427813419, ; 384: hi\Microsoft.Maui.Controls.resources => 0x90b57e2b => 322
	i32 2435356389, ; 385: System.Console.dll => 0x912896e5 => 20
	i32 2435904999, ; 386: System.ComponentModel.DataAnnotations.dll => 0x9130f5e7 => 14
	i32 2454642406, ; 387: System.Text.Encoding.dll => 0x924edee6 => 134
	i32 2458678730, ; 388: System.Net.Sockets.dll => 0x928c75ca => 74
	i32 2459001652, ; 389: System.Linq.Parallel.dll => 0x92916334 => 58
	i32 2465273461, ; 390: SQLitePCLRaw.batteries_v2.dll => 0x92f11675 => 210
	i32 2465532216, ; 391: Xamarin.AndroidX.ConstraintLayout.Core.dll => 0x92f50938 => 242
	i32 2471841756, ; 392: netstandard.dll => 0x93554fdc => 164
	i32 2475788418, ; 393: Java.Interop.dll => 0x93918882 => 165
	i32 2480646305, ; 394: Microsoft.Maui.Controls => 0x93dba8a1 => 199
	i32 2483903535, ; 395: System.ComponentModel.EventBasedAsync => 0x940d5c2f => 15
	i32 2484371297, ; 396: System.Net.ServicePoint => 0x94147f61 => 73
	i32 2486824558, ; 397: K4os.Hash.xxHash.dll => 0x9439ee6e => 180
	i32 2490993605, ; 398: System.AppContext.dll => 0x94798bc5 => 6
	i32 2498657740, ; 399: BouncyCastle.Cryptography.dll => 0x94ee7dcc => 170
	i32 2501346920, ; 400: System.Data.DataSetExtensions => 0x95178668 => 23
	i32 2505896520, ; 401: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x955cf248 => 264
	i32 2509217888, ; 402: System.Diagnostics.EventLog => 0x958fa060 => 216
	i32 2522472828, ; 403: Xamarin.Android.Glide.dll => 0x9659e17c => 223
	i32 2538310050, ; 404: System.Reflection.Emit.Lightweight.dll => 0x974b89a2 => 90
	i32 2550873716, ; 405: hr\Microsoft.Maui.Controls.resources => 0x980b3e74 => 323
	i32 2562349572, ; 406: Microsoft.CSharp => 0x98ba5a04 => 1
	i32 2570120770, ; 407: System.Text.Encodings.Web => 0x9930ee42 => 220
	i32 2581783588, ; 408: Xamarin.AndroidX.Lifecycle.Runtime.Ktx => 0x99e2e424 => 265
	i32 2581819634, ; 409: Xamarin.AndroidX.VectorDrawable.dll => 0x99e370f2 => 287
	i32 2585220780, ; 410: System.Text.Encoding.Extensions.dll => 0x9a1756ac => 133
	i32 2585805581, ; 411: System.Net.Ping => 0x9a20430d => 68
	i32 2589602615, ; 412: System.Threading.ThreadPool => 0x9a5a3337 => 143
	i32 2593496499, ; 413: pl\Microsoft.Maui.Controls.resources => 0x9a959db3 => 332
	i32 2605712449, ; 414: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 309
	i32 2611359322, ; 415: ZstdSharp.dll => 0x9ba62e5a => 310
	i32 2615233544, ; 416: Xamarin.AndroidX.Fragment.Ktx => 0x9be14c08 => 256
	i32 2616218305, ; 417: Microsoft.Extensions.Logging.Debug.dll => 0x9bf052c1 => 195
	i32 2617129537, ; 418: System.Private.Xml.dll => 0x9bfe3a41 => 87
	i32 2618712057, ; 419: System.Reflection.TypeExtensions.dll => 0x9c165ff9 => 95
	i32 2620871830, ; 420: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 246
	i32 2624644809, ; 421: Xamarin.AndroidX.DynamicAnimation => 0x9c70e6c9 => 251
	i32 2626831493, ; 422: ja\Microsoft.Maui.Controls.resources => 0x9c924485 => 327
	i32 2627185994, ; 423: System.Diagnostics.TextWriterTraceListener.dll => 0x9c97ad4a => 30
	i32 2629843544, ; 424: System.IO.Compression.ZipFile.dll => 0x9cc03a58 => 44
	i32 2633051222, ; 425: Xamarin.AndroidX.Lifecycle.LiveData => 0x9cf12c56 => 260
	i32 2634653062, ; 426: Microsoft.EntityFrameworkCore.Relational.dll => 0x9d099d86 => 184
	i32 2645932433, ; 427: FluentMigrator.Runner.Core => 0x9db5b991 => 174
	i32 2660759594, ; 428: System.Security.Cryptography.ProtectedData.dll => 0x9e97f82a => 218
	i32 2663391936, ; 429: Xamarin.Android.Glide.DiskLruCache => 0x9ec022c0 => 225
	i32 2663698177, ; 430: System.Runtime.Loader => 0x9ec4cf01 => 108
	i32 2664396074, ; 431: System.Xml.XDocument.dll => 0x9ecf752a => 155
	i32 2665622720, ; 432: System.Drawing.Primitives => 0x9ee22cc0 => 34
	i32 2676780864, ; 433: System.Data.Common.dll => 0x9f8c6f40 => 22
	i32 2686887180, ; 434: System.Runtime.Serialization.Xml.dll => 0xa026a50c => 113
	i32 2693849962, ; 435: System.IO.dll => 0xa090e36a => 56
	i32 2701096212, ; 436: Xamarin.AndroidX.Tracing.Tracing => 0xa0ff7514 => 285
	i32 2715334215, ; 437: System.Threading.Tasks.dll => 0xa1d8b647 => 141
	i32 2717744543, ; 438: System.Security.Claims => 0xa1fd7d9f => 117
	i32 2719963679, ; 439: System.Security.Cryptography.Cng.dll => 0xa21f5a1f => 119
	i32 2724373263, ; 440: System.Runtime.Numerics.dll => 0xa262a30f => 109
	i32 2732626843, ; 441: Xamarin.AndroidX.Activity => 0xa2e0939b => 227
	i32 2735172069, ; 442: System.Threading.Channels => 0xa30769e5 => 136
	i32 2737747696, ; 443: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 233
	i32 2740948882, ; 444: System.IO.Pipes.AccessControl => 0xa35f8f92 => 53
	i32 2748088231, ; 445: System.Runtime.InteropServices.JavaScript => 0xa3cc7fa7 => 104
	i32 2752995522, ; 446: pt-BR\Microsoft.Maui.Controls.resources => 0xa41760c2 => 333
	i32 2758225723, ; 447: Microsoft.Maui.Controls.Xaml => 0xa4672f3b => 200
	i32 2764765095, ; 448: Microsoft.Maui.dll => 0xa4caf7a7 => 201
	i32 2765824710, ; 449: System.Text.Encoding.CodePages.dll => 0xa4db22c6 => 132
	i32 2770495804, ; 450: Xamarin.Jetbrains.Annotations.dll => 0xa522693c => 303
	i32 2777249814, ; 451: AppInCube => 0xa5897816 => 0
	i32 2778768386, ; 452: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 290
	i32 2779977773, ; 453: Xamarin.AndroidX.ResourceInspection.Annotation.dll => 0xa5b3182d => 278
	i32 2785988530, ; 454: th\Microsoft.Maui.Controls.resources => 0xa60ecfb2 => 339
	i32 2788224221, ; 455: Xamarin.AndroidX.Fragment.Ktx.dll => 0xa630ecdd => 256
	i32 2801831435, ; 456: Microsoft.Maui.Graphics => 0xa7008e0b => 203
	i32 2803228030, ; 457: System.Xml.XPath.XDocument.dll => 0xa715dd7e => 156
	i32 2806116107, ; 458: es/Microsoft.Maui.Controls.resources.dll => 0xa741ef0b => 318
	i32 2810250172, ; 459: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 243
	i32 2819470561, ; 460: System.Xml.dll => 0xa80db4e1 => 160
	i32 2821205001, ; 461: System.ServiceProcess.dll => 0xa8282c09 => 131
	i32 2821294376, ; 462: Xamarin.AndroidX.ResourceInspection.Annotation => 0xa8298928 => 278
	i32 2824502124, ; 463: System.Xml.XmlDocument => 0xa85a7b6c => 158
	i32 2831556043, ; 464: nl/Microsoft.Maui.Controls.resources.dll => 0xa8c61dcb => 331
	i32 2833477479, ; 465: AppInCube.dll => 0xa8e36f67 => 0
	i32 2838993487, ; 466: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx.dll => 0xa9379a4f => 267
	i32 2841355853, ; 467: System.Security.Permissions => 0xa95ba64d => 219
	i32 2847418871, ; 468: Xamarin.GooglePlayServices.Base => 0xa9b829f7 => 299
	i32 2847789619, ; 469: Microsoft.EntityFrameworkCore.Relational => 0xa9bdd233 => 184
	i32 2849599387, ; 470: System.Threading.Overlapped.dll => 0xa9d96f9b => 137
	i32 2849763271, ; 471: de-DE\FluentMigrator.Abstractions.resources => 0xa9dbefc7 => 311
	i32 2853208004, ; 472: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 290
	i32 2855708567, ; 473: Xamarin.AndroidX.Transition => 0xaa36a797 => 286
	i32 2861098320, ; 474: Mono.Android.Export.dll => 0xaa88e550 => 166
	i32 2861189240, ; 475: Microsoft.Maui.Essentials => 0xaa8a4878 => 202
	i32 2867946736, ; 476: System.Security.Cryptography.ProtectedData => 0xaaf164f0 => 218
	i32 2870099610, ; 477: Xamarin.AndroidX.Activity.Ktx.dll => 0xab123e9a => 228
	i32 2875164099, ; 478: Jsr305Binding.dll => 0xab5f85c3 => 295
	i32 2875220617, ; 479: System.Globalization.Calendars.dll => 0xab606289 => 39
	i32 2884993177, ; 480: Xamarin.AndroidX.ExifInterface => 0xabf58099 => 254
	i32 2887636118, ; 481: System.Net.dll => 0xac1dd496 => 80
	i32 2899753641, ; 482: System.IO.UnmanagedMemoryStream => 0xacd6baa9 => 55
	i32 2900621748, ; 483: System.Dynamic.Runtime.dll => 0xace3f9b4 => 36
	i32 2901442782, ; 484: System.Reflection => 0xacf080de => 96
	i32 2905242038, ; 485: mscorlib.dll => 0xad2a79b6 => 163
	i32 2909740682, ; 486: System.Private.CoreLib => 0xad6f1e8a => 169
	i32 2916838712, ; 487: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 291
	i32 2919462931, ; 488: System.Numerics.Vectors.dll => 0xae037813 => 81
	i32 2921128767, ; 489: Xamarin.AndroidX.Annotation.Experimental.dll => 0xae1ce33f => 230
	i32 2936416060, ; 490: System.Resources.Reader => 0xaf06273c => 97
	i32 2940926066, ; 491: System.Diagnostics.StackTrace.dll => 0xaf4af872 => 29
	i32 2942453041, ; 492: System.Xml.XPath.XDocument => 0xaf624531 => 156
	i32 2944313911, ; 493: System.Configuration.ConfigurationManager.dll => 0xaf7eaa37 => 214
	i32 2959614098, ; 494: System.ComponentModel.dll => 0xb0682092 => 18
	i32 2968338931, ; 495: System.Security.Principal.Windows => 0xb0ed41f3 => 126
	i32 2972252294, ; 496: System.Security.Cryptography.Algorithms.dll => 0xb128f886 => 118
	i32 2978675010, ; 497: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 250
	i32 2987532451, ; 498: Xamarin.AndroidX.Security.SecurityCrypto => 0xb21220a3 => 281
	i32 2996846495, ; 499: Xamarin.AndroidX.Lifecycle.Process.dll => 0xb2a03f9f => 263
	i32 3012788804, ; 500: System.Configuration.ConfigurationManager => 0xb3938244 => 214
	i32 3016983068, ; 501: Xamarin.AndroidX.Startup.StartupRuntime => 0xb3d3821c => 283
	i32 3023353419, ; 502: WindowsBase.dll => 0xb434b64b => 162
	i32 3024354802, ; 503: Xamarin.AndroidX.Legacy.Support.Core.Utils => 0xb443fdf2 => 258
	i32 3025069135, ; 504: K4os.Compression.LZ4.Streams.dll => 0xb44ee44f => 179
	i32 3038032645, ; 505: _Microsoft.Android.Resource.Designer.dll => 0xb514b305 => 349
	i32 3056245963, ; 506: Xamarin.AndroidX.SavedState.SavedState.Ktx => 0xb62a9ccb => 280
	i32 3057625584, ; 507: Xamarin.AndroidX.Navigation.Common => 0xb63fa9f0 => 271
	i32 3058099980, ; 508: Xamarin.GooglePlayServices.Tasks => 0xb646e70c => 302
	i32 3059408633, ; 509: Mono.Android.Runtime => 0xb65adef9 => 167
	i32 3059793426, ; 510: System.ComponentModel.Primitives => 0xb660be12 => 16
	i32 3069363400, ; 511: Microsoft.Extensions.Caching.Abstractions.dll => 0xb6f2c4c8 => 186
	i32 3075834255, ; 512: System.Threading.Tasks => 0xb755818f => 141
	i32 3077302341, ; 513: hu/Microsoft.Maui.Controls.resources.dll => 0xb76be845 => 324
	i32 3085113210, ; 514: FluentMigrator.Runner.MySql.dll => 0xb7e3177a => 175
	i32 3089219899, ; 515: ZstdSharp => 0xb821c13b => 310
	i32 3090735792, ; 516: System.Security.Cryptography.X509Certificates.dll => 0xb838e2b0 => 124
	i32 3099732863, ; 517: System.Security.Claims.dll => 0xb8c22b7f => 117
	i32 3103600923, ; 518: System.Formats.Asn1 => 0xb8fd311b => 37
	i32 3111772706, ; 519: System.Runtime.Serialization => 0xb979e222 => 114
	i32 3121463068, ; 520: System.IO.FileSystem.AccessControl.dll => 0xba0dbf1c => 46
	i32 3124832203, ; 521: System.Threading.Tasks.Extensions => 0xba4127cb => 139
	i32 3132293585, ; 522: System.Security.AccessControl => 0xbab301d1 => 116
	i32 3147165239, ; 523: System.Diagnostics.Tracing.dll => 0xbb95ee37 => 33
	i32 3148237826, ; 524: GoogleGson.dll => 0xbba64c02 => 177
	i32 3159123045, ; 525: System.Reflection.Primitives.dll => 0xbc4c6465 => 94
	i32 3160747431, ; 526: System.IO.MemoryMappedFiles => 0xbc652da7 => 52
	i32 3170540552, ; 527: FluentMigrator.Runner.Core.dll => 0xbcfa9c08 => 174
	i32 3178803400, ; 528: Xamarin.AndroidX.Navigation.Fragment.dll => 0xbd78b0c8 => 272
	i32 3192346100, ; 529: System.Security.SecureString => 0xbe4755f4 => 128
	i32 3193515020, ; 530: System.Web => 0xbe592c0c => 150
	i32 3195844289, ; 531: Microsoft.Extensions.Caching.Abstractions => 0xbe7cb6c1 => 186
	i32 3204380047, ; 532: System.Data.dll => 0xbefef58f => 24
	i32 3209718065, ; 533: System.Xml.XmlDocument.dll => 0xbf506931 => 158
	i32 3211777861, ; 534: Xamarin.AndroidX.DocumentFile => 0xbf6fd745 => 249
	i32 3213246214, ; 535: System.Security.Permissions.dll => 0xbf863f06 => 219
	i32 3220365878, ; 536: System.Threading => 0xbff2e236 => 145
	i32 3226221578, ; 537: System.Runtime.Handles.dll => 0xc04c3c0a => 103
	i32 3230466174, ; 538: Xamarin.GooglePlayServices.Basement.dll => 0xc08d007e => 300
	i32 3251039220, ; 539: System.Reflection.DispatchProxy.dll => 0xc1c6ebf4 => 88
	i32 3258312781, ; 540: Xamarin.AndroidX.CardView => 0xc235e84d => 237
	i32 3265493905, ; 541: System.Linq.Queryable.dll => 0xc2a37b91 => 59
	i32 3265893370, ; 542: System.Threading.Tasks.Extensions.dll => 0xc2a993fa => 139
	i32 3277815716, ; 543: System.Resources.Writer.dll => 0xc35f7fa4 => 99
	i32 3279906254, ; 544: Microsoft.Win32.Registry.dll => 0xc37f65ce => 5
	i32 3280506390, ; 545: System.ComponentModel.Annotations.dll => 0xc3888e16 => 13
	i32 3286872994, ; 546: SQLite-net.dll => 0xc3e9b3a2 => 209
	i32 3290767353, ; 547: System.Security.Cryptography.Encoding => 0xc4251ff9 => 121
	i32 3299363146, ; 548: System.Text.Encoding => 0xc4a8494a => 134
	i32 3303498502, ; 549: System.Diagnostics.FileVersionInfo => 0xc4e76306 => 27
	i32 3305363605, ; 550: fi\Microsoft.Maui.Controls.resources => 0xc503d895 => 319
	i32 3316684772, ; 551: System.Net.Requests.dll => 0xc5b097e4 => 71
	i32 3317135071, ; 552: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 247
	i32 3317144872, ; 553: System.Data => 0xc5b79d28 => 24
	i32 3340431453, ; 554: Xamarin.AndroidX.Arch.Core.Runtime => 0xc71af05d => 235
	i32 3345895724, ; 555: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 0xc76e512c => 276
	i32 3346324047, ; 556: Xamarin.AndroidX.Navigation.Runtime => 0xc774da4f => 273
	i32 3357674450, ; 557: ru\Microsoft.Maui.Controls.resources => 0xc8220bd2 => 336
	i32 3358260929, ; 558: System.Text.Json => 0xc82afec1 => 221
	i32 3360279109, ; 559: SQLitePCLRaw.core => 0xc849ca45 => 211
	i32 3362336904, ; 560: Xamarin.AndroidX.Activity.Ktx => 0xc8693088 => 228
	i32 3362522851, ; 561: Xamarin.AndroidX.Core => 0xc86c06e3 => 244
	i32 3366347497, ; 562: Java.Interop => 0xc8a662e9 => 165
	i32 3374999561, ; 563: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 277
	i32 3381016424, ; 564: da\Microsoft.Maui.Controls.resources => 0xc9863768 => 315
	i32 3381033598, ; 565: K4os.Compression.LZ4 => 0xc9867a7e => 178
	i32 3395150330, ; 566: System.Runtime.CompilerServices.Unsafe.dll => 0xca5de1fa => 100
	i32 3403906625, ; 567: System.Security.Cryptography.OpenSsl.dll => 0xcae37e41 => 122
	i32 3405233483, ; 568: Xamarin.AndroidX.CustomView.PoolingContainer => 0xcaf7bd4b => 248
	i32 3428513518, ; 569: Microsoft.Extensions.DependencyInjection.dll => 0xcc5af6ee => 190
	i32 3429136800, ; 570: System.Xml => 0xcc6479a0 => 160
	i32 3430777524, ; 571: netstandard => 0xcc7d82b4 => 164
	i32 3441283291, ; 572: Xamarin.AndroidX.DynamicAnimation.dll => 0xcd1dd0db => 251
	i32 3445260447, ; 573: System.Formats.Tar => 0xcd5a809f => 38
	i32 3452344032, ; 574: Microsoft.Maui.Controls.Compatibility.dll => 0xcdc696e0 => 198
	i32 3463511458, ; 575: hr/Microsoft.Maui.Controls.resources.dll => 0xce70fda2 => 323
	i32 3467345667, ; 576: MySql.Data => 0xceab7f03 => 205
	i32 3471940407, ; 577: System.ComponentModel.TypeConverter.dll => 0xcef19b37 => 17
	i32 3476120550, ; 578: Mono.Android => 0xcf3163e6 => 168
	i32 3479583265, ; 579: ru/Microsoft.Maui.Controls.resources.dll => 0xcf663a21 => 336
	i32 3484440000, ; 580: ro\Microsoft.Maui.Controls.resources => 0xcfb055c0 => 335
	i32 3485117614, ; 581: System.Text.Json.dll => 0xcfbaacae => 221
	i32 3486566296, ; 582: System.Transactions => 0xcfd0c798 => 147
	i32 3493954962, ; 583: Xamarin.AndroidX.Concurrent.Futures.dll => 0xd0418592 => 240
	i32 3494395880, ; 584: Xamarin.GooglePlayServices.Location.dll => 0xd0483fe8 => 301
	i32 3499097210, ; 585: Google.Protobuf.dll => 0xd08ffc7a => 176
	i32 3509114376, ; 586: System.Xml.Linq => 0xd128d608 => 152
	i32 3515174580, ; 587: System.Security.dll => 0xd1854eb4 => 129
	i32 3530912306, ; 588: System.Configuration => 0xd2757232 => 19
	i32 3539954161, ; 589: System.Net.HttpListener => 0xd2ff69f1 => 64
	i32 3560100363, ; 590: System.Threading.Timer => 0xd432d20b => 144
	i32 3570554715, ; 591: System.IO.FileSystem.AccessControl => 0xd4d2575b => 46
	i32 3580758918, ; 592: zh-HK\Microsoft.Maui.Controls.resources => 0xd56e0b86 => 343
	i32 3593904229, ; 593: FluentMigrator.Extensions.MySql.dll => 0xd636a065 => 173
	i32 3597029428, ; 594: Xamarin.Android.Glide.GifDecoder.dll => 0xd6665034 => 226
	i32 3598340787, ; 595: System.Net.WebSockets.Client => 0xd67a52b3 => 78
	i32 3605570793, ; 596: BouncyCastle.Cryptography => 0xd6e8a4e9 => 170
	i32 3608519521, ; 597: System.Linq.dll => 0xd715a361 => 60
	i32 3624195450, ; 598: System.Runtime.InteropServices.RuntimeInformation => 0xd804d57a => 105
	i32 3627220390, ; 599: Xamarin.AndroidX.Print.dll => 0xd832fda6 => 275
	i32 3629049339, ; 600: Microsoft.Toolkit.Uwp.Notifications.dll => 0xd84ee5fb => 204
	i32 3633644679, ; 601: Xamarin.AndroidX.Annotation.Experimental => 0xd8950487 => 230
	i32 3638274909, ; 602: System.IO.FileSystem.Primitives.dll => 0xd8dbab5d => 48
	i32 3641597786, ; 603: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 261
	i32 3643446276, ; 604: tr\Microsoft.Maui.Controls.resources => 0xd92a9404 => 340
	i32 3643854240, ; 605: Xamarin.AndroidX.Navigation.Fragment => 0xd930cda0 => 272
	i32 3645089577, ; 606: System.ComponentModel.DataAnnotations => 0xd943a729 => 14
	i32 3645630983, ; 607: Google.Protobuf => 0xd94bea07 => 176
	i32 3657292374, ; 608: Microsoft.Extensions.Configuration.Abstractions.dll => 0xd9fdda56 => 189
	i32 3660523487, ; 609: System.Net.NetworkInformation => 0xda2f27df => 67
	i32 3672681054, ; 610: Mono.Android.dll => 0xdae8aa5e => 168
	i32 3676670898, ; 611: Microsoft.Maui.Controls.HotReload.Forms => 0xdb258bb2 => 346
	i32 3682565725, ; 612: Xamarin.AndroidX.Browser => 0xdb7f7e5d => 236
	i32 3684561358, ; 613: Xamarin.AndroidX.Concurrent.Futures => 0xdb9df1ce => 240
	i32 3697841164, ; 614: zh-Hant/Microsoft.Maui.Controls.resources.dll => 0xdc68940c => 345
	i32 3700866549, ; 615: System.Net.WebProxy.dll => 0xdc96bdf5 => 77
	i32 3706696989, ; 616: Xamarin.AndroidX.Core.Core.Ktx.dll => 0xdcefb51d => 245
	i32 3716563718, ; 617: System.Runtime.Intrinsics => 0xdd864306 => 107
	i32 3718780102, ; 618: Xamarin.AndroidX.Annotation => 0xdda814c6 => 229
	i32 3724971120, ; 619: Xamarin.AndroidX.Navigation.Common.dll => 0xde068c70 => 271
	i32 3732100267, ; 620: System.Net.NameResolution => 0xde7354ab => 66
	i32 3737834244, ; 621: System.Net.Http.Json.dll => 0xdecad304 => 62
	i32 3748608112, ; 622: System.Diagnostics.DiagnosticSource => 0xdf6f3870 => 215
	i32 3751444290, ; 623: System.Xml.XPath => 0xdf9a7f42 => 157
	i32 3754567612, ; 624: SQLitePCLRaw.provider.e_sqlite3 => 0xdfca27bc => 213
	i32 3786282454, ; 625: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 238
	i32 3792276235, ; 626: System.Collections.NonGeneric => 0xe2098b0b => 10
	i32 3800979733, ; 627: Microsoft.Maui.Controls.Compatibility => 0xe28e5915 => 198
	i32 3802395368, ; 628: System.Collections.Specialized.dll => 0xe2a3f2e8 => 11
	i32 3819260425, ; 629: System.Net.WebProxy => 0xe3a54a09 => 77
	i32 3823082795, ; 630: System.Security.Cryptography.dll => 0xe3df9d2b => 125
	i32 3829621856, ; 631: System.Numerics.dll => 0xe4436460 => 82
	i32 3841636137, ; 632: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xe4fab729 => 191
	i32 3844307129, ; 633: System.Net.Mail.dll => 0xe52378b9 => 65
	i32 3849253459, ; 634: System.Runtime.InteropServices.dll => 0xe56ef253 => 106
	i32 3870376305, ; 635: System.Net.HttpListener.dll => 0xe6b14171 => 64
	i32 3873536506, ; 636: System.Security.Principal => 0xe6e179fa => 127
	i32 3875112723, ; 637: System.Security.Cryptography.Encoding.dll => 0xe6f98713 => 121
	i32 3876362041, ; 638: SQLite-net => 0xe70c9739 => 209
	i32 3885497537, ; 639: System.Net.WebHeaderCollection.dll => 0xe797fcc1 => 76
	i32 3885922214, ; 640: Xamarin.AndroidX.Transition.dll => 0xe79e77a6 => 286
	i32 3888767677, ; 641: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 0xe7c9e2bd => 276
	i32 3889960447, ; 642: zh-Hans/Microsoft.Maui.Controls.resources.dll => 0xe7dc15ff => 344
	i32 3896106733, ; 643: System.Collections.Concurrent.dll => 0xe839deed => 8
	i32 3896760992, ; 644: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 244
	i32 3901907137, ; 645: Microsoft.VisualBasic.Core.dll => 0xe89260c1 => 2
	i32 3920810846, ; 646: System.IO.Compression.FileSystem.dll => 0xe9b2d35e => 43
	i32 3921031405, ; 647: Xamarin.AndroidX.VersionedParcelable.dll => 0xe9b630ed => 289
	i32 3928044579, ; 648: System.Xml.ReaderWriter => 0xea213423 => 153
	i32 3930554604, ; 649: System.Security.Principal.dll => 0xea4780ec => 127
	i32 3931092270, ; 650: Xamarin.AndroidX.Navigation.UI => 0xea4fb52e => 274
	i32 3945713374, ; 651: System.Data.DataSetExtensions.dll => 0xeb2ecede => 23
	i32 3953953790, ; 652: System.Text.Encoding.CodePages => 0xebac8bfe => 132
	i32 3955647286, ; 653: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 232
	i32 3959773229, ; 654: Xamarin.AndroidX.Lifecycle.Process => 0xec05582d => 263
	i32 3966452346, ; 655: FluentMigrator.dll => 0xec6b427a => 171
	i32 3967165417, ; 656: Xamarin.GooglePlayServices.Location => 0xec7623e9 => 301
	i32 3970018735, ; 657: Xamarin.GooglePlayServices.Tasks.dll => 0xeca1adaf => 302
	i32 3980434154, ; 658: th/Microsoft.Maui.Controls.resources.dll => 0xed409aea => 339
	i32 3987592930, ; 659: he/Microsoft.Maui.Controls.resources.dll => 0xedadd6e2 => 321
	i32 4003436829, ; 660: System.Diagnostics.Process.dll => 0xee9f991d => 28
	i32 4015948917, ; 661: Xamarin.AndroidX.Annotation.Jvm.dll => 0xef5e8475 => 231
	i32 4023392905, ; 662: System.IO.Pipelines => 0xefd01a89 => 217
	i32 4025784931, ; 663: System.Memory => 0xeff49a63 => 61
	i32 4046471985, ; 664: Microsoft.Maui.Controls.Xaml.dll => 0xf1304331 => 200
	i32 4054681211, ; 665: System.Reflection.Emit.ILGeneration => 0xf1ad867b => 89
	i32 4068434129, ; 666: System.Private.Xml.Linq.dll => 0xf27f60d1 => 86
	i32 4073602200, ; 667: System.Threading.dll => 0xf2ce3c98 => 145
	i32 4079385022, ; 668: MySqlConnector.dll => 0xf32679be => 206
	i32 4094352644, ; 669: Microsoft.Maui.Essentials.dll => 0xf40add04 => 202
	i32 4099507663, ; 670: System.Drawing.dll => 0xf45985cf => 35
	i32 4100113165, ; 671: System.Private.Uri => 0xf462c30d => 85
	i32 4101593132, ; 672: Xamarin.AndroidX.Emoji2 => 0xf479582c => 252
	i32 4101842092, ; 673: Microsoft.Extensions.Caching.Memory => 0xf47d24ac => 187
	i32 4102112229, ; 674: pt/Microsoft.Maui.Controls.resources.dll => 0xf48143e5 => 334
	i32 4125707920, ; 675: ms/Microsoft.Maui.Controls.resources.dll => 0xf5e94e90 => 329
	i32 4126470640, ; 676: Microsoft.Extensions.DependencyInjection => 0xf5f4f1f0 => 190
	i32 4127667938, ; 677: System.IO.FileSystem.Watcher => 0xf60736e2 => 49
	i32 4130442656, ; 678: System.AppContext => 0xf6318da0 => 6
	i32 4135660637, ; 679: FluentMigrator.Extensions.MySql => 0xf6812c5d => 173
	i32 4147896353, ; 680: System.Reflection.Emit.ILGeneration.dll => 0xf73be021 => 89
	i32 4150914736, ; 681: uk\Microsoft.Maui.Controls.resources => 0xf769eeb0 => 341
	i32 4151237749, ; 682: System.Core => 0xf76edc75 => 21
	i32 4159265925, ; 683: System.Xml.XmlSerializer => 0xf7e95c85 => 159
	i32 4161255271, ; 684: System.Reflection.TypeExtensions => 0xf807b767 => 95
	i32 4164802419, ; 685: System.IO.FileSystem.Watcher.dll => 0xf83dd773 => 49
	i32 4167813518, ; 686: FluentMigrator.Abstractions => 0xf86bc98e => 172
	i32 4181436372, ; 687: System.Runtime.Serialization.Primitives => 0xf93ba7d4 => 112
	i32 4182413190, ; 688: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 268
	i32 4182880526, ; 689: Microsoft.VisualStudio.DesignTools.MobileTapContracts => 0xf951b10e => 347
	i32 4185676441, ; 690: System.Security => 0xf97c5a99 => 129
	i32 4196529839, ; 691: System.Net.WebClient.dll => 0xfa21f6af => 75
	i32 4213026141, ; 692: System.Diagnostics.DiagnosticSource.dll => 0xfb1dad5d => 215
	i32 4256097574, ; 693: Xamarin.AndroidX.Core.Core.Ktx => 0xfdaee526 => 245
	i32 4258378803, ; 694: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx => 0xfdd1b433 => 267
	i32 4260525087, ; 695: System.Buffers => 0xfdf2741f => 7
	i32 4271975918, ; 696: Microsoft.Maui.Controls.dll => 0xfea12dee => 199
	i32 4274976490, ; 697: System.Runtime.Numerics => 0xfecef6ea => 109
	i32 4292120959, ; 698: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 268
	i32 4294763496 ; 699: Xamarin.AndroidX.ExifInterface.dll => 0xfffce3e8 => 254
], align 4

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [700 x i32] [
	i32 67, ; 0
	i32 66, ; 1
	i32 107, ; 2
	i32 192, ; 3
	i32 264, ; 4
	i32 298, ; 5
	i32 47, ; 6
	i32 207, ; 7
	i32 79, ; 8
	i32 142, ; 9
	i32 29, ; 10
	i32 345, ; 11
	i32 123, ; 12
	i32 203, ; 13
	i32 101, ; 14
	i32 282, ; 15
	i32 106, ; 16
	i32 282, ; 17
	i32 136, ; 18
	i32 306, ; 19
	i32 76, ; 20
	i32 123, ; 21
	i32 13, ; 22
	i32 238, ; 23
	i32 131, ; 24
	i32 284, ; 25
	i32 148, ; 26
	i32 342, ; 27
	i32 343, ; 28
	i32 18, ; 29
	i32 236, ; 30
	i32 26, ; 31
	i32 258, ; 32
	i32 1, ; 33
	i32 58, ; 34
	i32 41, ; 35
	i32 90, ; 36
	i32 241, ; 37
	i32 144, ; 38
	i32 260, ; 39
	i32 257, ; 40
	i32 314, ; 41
	i32 53, ; 42
	i32 68, ; 43
	i32 342, ; 44
	i32 227, ; 45
	i32 82, ; 46
	i32 327, ; 47
	i32 259, ; 48
	i32 212, ; 49
	i32 326, ; 50
	i32 130, ; 51
	i32 54, ; 52
	i32 146, ; 53
	i32 73, ; 54
	i32 142, ; 55
	i32 61, ; 56
	i32 143, ; 57
	i32 349, ; 58
	i32 162, ; 59
	i32 338, ; 60
	i32 204, ; 61
	i32 242, ; 62
	i32 12, ; 63
	i32 255, ; 64
	i32 124, ; 65
	i32 149, ; 66
	i32 112, ; 67
	i32 163, ; 68
	i32 161, ; 69
	i32 257, ; 70
	i32 270, ; 71
	i32 83, ; 72
	i32 325, ; 73
	i32 319, ; 74
	i32 208, ; 75
	i32 197, ; 76
	i32 147, ; 77
	i32 306, ; 78
	i32 59, ; 79
	i32 193, ; 80
	i32 50, ; 81
	i32 102, ; 82
	i32 113, ; 83
	i32 39, ; 84
	i32 295, ; 85
	i32 293, ; 86
	i32 119, ; 87
	i32 333, ; 88
	i32 51, ; 89
	i32 43, ; 90
	i32 118, ; 91
	i32 179, ; 92
	i32 247, ; 93
	i32 331, ; 94
	i32 253, ; 95
	i32 80, ; 96
	i32 220, ; 97
	i32 289, ; 98
	i32 234, ; 99
	i32 8, ; 100
	i32 72, ; 101
	i32 313, ; 102
	i32 152, ; 103
	i32 308, ; 104
	i32 151, ; 105
	i32 91, ; 106
	i32 303, ; 107
	i32 44, ; 108
	i32 328, ; 109
	i32 316, ; 110
	i32 307, ; 111
	i32 108, ; 112
	i32 128, ; 113
	i32 311, ; 114
	i32 210, ; 115
	i32 25, ; 116
	i32 224, ; 117
	i32 71, ; 118
	i32 54, ; 119
	i32 45, ; 120
	i32 337, ; 121
	i32 196, ; 122
	i32 248, ; 123
	i32 22, ; 124
	i32 262, ; 125
	i32 85, ; 126
	i32 42, ; 127
	i32 157, ; 128
	i32 70, ; 129
	i32 275, ; 130
	i32 3, ; 131
	i32 41, ; 132
	i32 62, ; 133
	i32 16, ; 134
	i32 52, ; 135
	i32 340, ; 136
	i32 298, ; 137
	i32 104, ; 138
	i32 207, ; 139
	i32 307, ; 140
	i32 296, ; 141
	i32 259, ; 142
	i32 33, ; 143
	i32 155, ; 144
	i32 205, ; 145
	i32 84, ; 146
	i32 31, ; 147
	i32 12, ; 148
	i32 50, ; 149
	i32 55, ; 150
	i32 279, ; 151
	i32 35, ; 152
	i32 191, ; 153
	i32 315, ; 154
	i32 297, ; 155
	i32 232, ; 156
	i32 34, ; 157
	i32 57, ; 158
	i32 266, ; 159
	i32 177, ; 160
	i32 17, ; 161
	i32 304, ; 162
	i32 216, ; 163
	i32 161, ; 164
	i32 328, ; 165
	i32 265, ; 166
	i32 195, ; 167
	i32 222, ; 168
	i32 292, ; 169
	i32 183, ; 170
	i32 172, ; 171
	i32 334, ; 172
	i32 150, ; 173
	i32 288, ; 174
	i32 273, ; 175
	i32 183, ; 176
	i32 332, ; 177
	i32 234, ; 178
	i32 187, ; 179
	i32 28, ; 180
	i32 51, ; 181
	i32 330, ; 182
	i32 293, ; 183
	i32 5, ; 184
	i32 314, ; 185
	i32 283, ; 186
	i32 287, ; 187
	i32 239, ; 188
	i32 308, ; 189
	i32 231, ; 190
	i32 211, ; 191
	i32 250, ; 192
	i32 84, ; 193
	i32 292, ; 194
	i32 60, ; 195
	i32 111, ; 196
	i32 56, ; 197
	i32 344, ; 198
	i32 279, ; 199
	i32 98, ; 200
	i32 19, ; 201
	i32 243, ; 202
	i32 110, ; 203
	i32 100, ; 204
	i32 101, ; 205
	i32 312, ; 206
	i32 103, ; 207
	i32 296, ; 208
	i32 70, ; 209
	i32 37, ; 210
	i32 31, ; 211
	i32 102, ; 212
	i32 72, ; 213
	i32 318, ; 214
	i32 9, ; 215
	i32 122, ; 216
	i32 45, ; 217
	i32 233, ; 218
	i32 197, ; 219
	i32 9, ; 220
	i32 42, ; 221
	i32 4, ; 222
	i32 180, ; 223
	i32 280, ; 224
	i32 181, ; 225
	i32 322, ; 226
	i32 206, ; 227
	i32 317, ; 228
	i32 208, ; 229
	i32 30, ; 230
	i32 135, ; 231
	i32 91, ; 232
	i32 92, ; 233
	i32 337, ; 234
	i32 48, ; 235
	i32 138, ; 236
	i32 111, ; 237
	i32 137, ; 238
	i32 249, ; 239
	i32 114, ; 240
	i32 297, ; 241
	i32 154, ; 242
	i32 346, ; 243
	i32 75, ; 244
	i32 78, ; 245
	i32 269, ; 246
	i32 36, ; 247
	i32 291, ; 248
	i32 253, ; 249
	i32 246, ; 250
	i32 63, ; 251
	i32 135, ; 252
	i32 15, ; 253
	i32 115, ; 254
	i32 285, ; 255
	i32 294, ; 256
	i32 241, ; 257
	i32 47, ; 258
	i32 69, ; 259
	i32 79, ; 260
	i32 125, ; 261
	i32 181, ; 262
	i32 182, ; 263
	i32 93, ; 264
	i32 120, ; 265
	i32 305, ; 266
	i32 26, ; 267
	i32 212, ; 268
	i32 262, ; 269
	i32 96, ; 270
	i32 27, ; 271
	i32 237, ; 272
	i32 335, ; 273
	i32 313, ; 274
	i32 146, ; 275
	i32 217, ; 276
	i32 166, ; 277
	i32 4, ; 278
	i32 97, ; 279
	i32 32, ; 280
	i32 92, ; 281
	i32 284, ; 282
	i32 193, ; 283
	i32 21, ; 284
	i32 40, ; 285
	i32 167, ; 286
	i32 329, ; 287
	i32 255, ; 288
	i32 321, ; 289
	i32 269, ; 290
	i32 304, ; 291
	i32 294, ; 292
	i32 274, ; 293
	i32 2, ; 294
	i32 133, ; 295
	i32 110, ; 296
	i32 348, ; 297
	i32 194, ; 298
	i32 222, ; 299
	i32 341, ; 300
	i32 224, ; 301
	i32 338, ; 302
	i32 57, ; 303
	i32 94, ; 304
	i32 320, ; 305
	i32 38, ; 306
	i32 235, ; 307
	i32 348, ; 308
	i32 185, ; 309
	i32 25, ; 310
	i32 93, ; 311
	i32 175, ; 312
	i32 88, ; 313
	i32 98, ; 314
	i32 300, ; 315
	i32 10, ; 316
	i32 178, ; 317
	i32 86, ; 318
	i32 99, ; 319
	i32 281, ; 320
	i32 188, ; 321
	i32 305, ; 322
	i32 226, ; 323
	i32 317, ; 324
	i32 7, ; 325
	i32 185, ; 326
	i32 266, ; 327
	i32 312, ; 328
	i32 223, ; 329
	i32 87, ; 330
	i32 261, ; 331
	i32 151, ; 332
	i32 316, ; 333
	i32 32, ; 334
	i32 115, ; 335
	i32 81, ; 336
	i32 213, ; 337
	i32 20, ; 338
	i32 299, ; 339
	i32 11, ; 340
	i32 159, ; 341
	i32 3, ; 342
	i32 201, ; 343
	i32 324, ; 344
	i32 171, ; 345
	i32 196, ; 346
	i32 194, ; 347
	i32 83, ; 348
	i32 192, ; 349
	i32 309, ; 350
	i32 63, ; 351
	i32 326, ; 352
	i32 288, ; 353
	i32 140, ; 354
	i32 270, ; 355
	i32 154, ; 356
	i32 182, ; 357
	i32 40, ; 358
	i32 116, ; 359
	i32 189, ; 360
	i32 225, ; 361
	i32 320, ; 362
	i32 277, ; 363
	i32 130, ; 364
	i32 74, ; 365
	i32 65, ; 366
	i32 330, ; 367
	i32 169, ; 368
	i32 229, ; 369
	i32 140, ; 370
	i32 105, ; 371
	i32 148, ; 372
	i32 69, ; 373
	i32 153, ; 374
	i32 188, ; 375
	i32 120, ; 376
	i32 126, ; 377
	i32 325, ; 378
	i32 149, ; 379
	i32 252, ; 380
	i32 347, ; 381
	i32 138, ; 382
	i32 239, ; 383
	i32 322, ; 384
	i32 20, ; 385
	i32 14, ; 386
	i32 134, ; 387
	i32 74, ; 388
	i32 58, ; 389
	i32 210, ; 390
	i32 242, ; 391
	i32 164, ; 392
	i32 165, ; 393
	i32 199, ; 394
	i32 15, ; 395
	i32 73, ; 396
	i32 180, ; 397
	i32 6, ; 398
	i32 170, ; 399
	i32 23, ; 400
	i32 264, ; 401
	i32 216, ; 402
	i32 223, ; 403
	i32 90, ; 404
	i32 323, ; 405
	i32 1, ; 406
	i32 220, ; 407
	i32 265, ; 408
	i32 287, ; 409
	i32 133, ; 410
	i32 68, ; 411
	i32 143, ; 412
	i32 332, ; 413
	i32 309, ; 414
	i32 310, ; 415
	i32 256, ; 416
	i32 195, ; 417
	i32 87, ; 418
	i32 95, ; 419
	i32 246, ; 420
	i32 251, ; 421
	i32 327, ; 422
	i32 30, ; 423
	i32 44, ; 424
	i32 260, ; 425
	i32 184, ; 426
	i32 174, ; 427
	i32 218, ; 428
	i32 225, ; 429
	i32 108, ; 430
	i32 155, ; 431
	i32 34, ; 432
	i32 22, ; 433
	i32 113, ; 434
	i32 56, ; 435
	i32 285, ; 436
	i32 141, ; 437
	i32 117, ; 438
	i32 119, ; 439
	i32 109, ; 440
	i32 227, ; 441
	i32 136, ; 442
	i32 233, ; 443
	i32 53, ; 444
	i32 104, ; 445
	i32 333, ; 446
	i32 200, ; 447
	i32 201, ; 448
	i32 132, ; 449
	i32 303, ; 450
	i32 0, ; 451
	i32 290, ; 452
	i32 278, ; 453
	i32 339, ; 454
	i32 256, ; 455
	i32 203, ; 456
	i32 156, ; 457
	i32 318, ; 458
	i32 243, ; 459
	i32 160, ; 460
	i32 131, ; 461
	i32 278, ; 462
	i32 158, ; 463
	i32 331, ; 464
	i32 0, ; 465
	i32 267, ; 466
	i32 219, ; 467
	i32 299, ; 468
	i32 184, ; 469
	i32 137, ; 470
	i32 311, ; 471
	i32 290, ; 472
	i32 286, ; 473
	i32 166, ; 474
	i32 202, ; 475
	i32 218, ; 476
	i32 228, ; 477
	i32 295, ; 478
	i32 39, ; 479
	i32 254, ; 480
	i32 80, ; 481
	i32 55, ; 482
	i32 36, ; 483
	i32 96, ; 484
	i32 163, ; 485
	i32 169, ; 486
	i32 291, ; 487
	i32 81, ; 488
	i32 230, ; 489
	i32 97, ; 490
	i32 29, ; 491
	i32 156, ; 492
	i32 214, ; 493
	i32 18, ; 494
	i32 126, ; 495
	i32 118, ; 496
	i32 250, ; 497
	i32 281, ; 498
	i32 263, ; 499
	i32 214, ; 500
	i32 283, ; 501
	i32 162, ; 502
	i32 258, ; 503
	i32 179, ; 504
	i32 349, ; 505
	i32 280, ; 506
	i32 271, ; 507
	i32 302, ; 508
	i32 167, ; 509
	i32 16, ; 510
	i32 186, ; 511
	i32 141, ; 512
	i32 324, ; 513
	i32 175, ; 514
	i32 310, ; 515
	i32 124, ; 516
	i32 117, ; 517
	i32 37, ; 518
	i32 114, ; 519
	i32 46, ; 520
	i32 139, ; 521
	i32 116, ; 522
	i32 33, ; 523
	i32 177, ; 524
	i32 94, ; 525
	i32 52, ; 526
	i32 174, ; 527
	i32 272, ; 528
	i32 128, ; 529
	i32 150, ; 530
	i32 186, ; 531
	i32 24, ; 532
	i32 158, ; 533
	i32 249, ; 534
	i32 219, ; 535
	i32 145, ; 536
	i32 103, ; 537
	i32 300, ; 538
	i32 88, ; 539
	i32 237, ; 540
	i32 59, ; 541
	i32 139, ; 542
	i32 99, ; 543
	i32 5, ; 544
	i32 13, ; 545
	i32 209, ; 546
	i32 121, ; 547
	i32 134, ; 548
	i32 27, ; 549
	i32 319, ; 550
	i32 71, ; 551
	i32 247, ; 552
	i32 24, ; 553
	i32 235, ; 554
	i32 276, ; 555
	i32 273, ; 556
	i32 336, ; 557
	i32 221, ; 558
	i32 211, ; 559
	i32 228, ; 560
	i32 244, ; 561
	i32 165, ; 562
	i32 277, ; 563
	i32 315, ; 564
	i32 178, ; 565
	i32 100, ; 566
	i32 122, ; 567
	i32 248, ; 568
	i32 190, ; 569
	i32 160, ; 570
	i32 164, ; 571
	i32 251, ; 572
	i32 38, ; 573
	i32 198, ; 574
	i32 323, ; 575
	i32 205, ; 576
	i32 17, ; 577
	i32 168, ; 578
	i32 336, ; 579
	i32 335, ; 580
	i32 221, ; 581
	i32 147, ; 582
	i32 240, ; 583
	i32 301, ; 584
	i32 176, ; 585
	i32 152, ; 586
	i32 129, ; 587
	i32 19, ; 588
	i32 64, ; 589
	i32 144, ; 590
	i32 46, ; 591
	i32 343, ; 592
	i32 173, ; 593
	i32 226, ; 594
	i32 78, ; 595
	i32 170, ; 596
	i32 60, ; 597
	i32 105, ; 598
	i32 275, ; 599
	i32 204, ; 600
	i32 230, ; 601
	i32 48, ; 602
	i32 261, ; 603
	i32 340, ; 604
	i32 272, ; 605
	i32 14, ; 606
	i32 176, ; 607
	i32 189, ; 608
	i32 67, ; 609
	i32 168, ; 610
	i32 346, ; 611
	i32 236, ; 612
	i32 240, ; 613
	i32 345, ; 614
	i32 77, ; 615
	i32 245, ; 616
	i32 107, ; 617
	i32 229, ; 618
	i32 271, ; 619
	i32 66, ; 620
	i32 62, ; 621
	i32 215, ; 622
	i32 157, ; 623
	i32 213, ; 624
	i32 238, ; 625
	i32 10, ; 626
	i32 198, ; 627
	i32 11, ; 628
	i32 77, ; 629
	i32 125, ; 630
	i32 82, ; 631
	i32 191, ; 632
	i32 65, ; 633
	i32 106, ; 634
	i32 64, ; 635
	i32 127, ; 636
	i32 121, ; 637
	i32 209, ; 638
	i32 76, ; 639
	i32 286, ; 640
	i32 276, ; 641
	i32 344, ; 642
	i32 8, ; 643
	i32 244, ; 644
	i32 2, ; 645
	i32 43, ; 646
	i32 289, ; 647
	i32 153, ; 648
	i32 127, ; 649
	i32 274, ; 650
	i32 23, ; 651
	i32 132, ; 652
	i32 232, ; 653
	i32 263, ; 654
	i32 171, ; 655
	i32 301, ; 656
	i32 302, ; 657
	i32 339, ; 658
	i32 321, ; 659
	i32 28, ; 660
	i32 231, ; 661
	i32 217, ; 662
	i32 61, ; 663
	i32 200, ; 664
	i32 89, ; 665
	i32 86, ; 666
	i32 145, ; 667
	i32 206, ; 668
	i32 202, ; 669
	i32 35, ; 670
	i32 85, ; 671
	i32 252, ; 672
	i32 187, ; 673
	i32 334, ; 674
	i32 329, ; 675
	i32 190, ; 676
	i32 49, ; 677
	i32 6, ; 678
	i32 173, ; 679
	i32 89, ; 680
	i32 341, ; 681
	i32 21, ; 682
	i32 159, ; 683
	i32 95, ; 684
	i32 49, ; 685
	i32 172, ; 686
	i32 112, ; 687
	i32 268, ; 688
	i32 347, ; 689
	i32 129, ; 690
	i32 75, ; 691
	i32 215, ; 692
	i32 245, ; 693
	i32 267, ; 694
	i32 7, ; 695
	i32 199, ; 696
	i32 109, ; 697
	i32 268, ; 698
	i32 254 ; 699
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
attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-thumb-mode,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-thumb-mode,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" }

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
!7 = !{i32 1, !"min_enum_size", i32 4}
