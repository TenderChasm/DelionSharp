using Delion.Utilities;
using System;

namespace Delion.Bindings.FreeType
{
    internal unsafe struct FT_MemoryRec_
    {
        public void* User;
        public delegate* unmanaged<FT_MemoryRec_*, int, void*> Alloc;
        public delegate* unmanaged<FT_MemoryRec_*, void*, void> Free;
        public delegate* unmanaged<FT_MemoryRec_*, int, int, void*, void*> Realloc;
    }

    internal unsafe struct FT_StreamRec_
    {
        public byte* Base;
        public uint Size;
        public uint Pos;
        public FT_StreamDesc_ Descriptor;
        public FT_StreamDesc_ Pathname;
        public delegate* unmanaged<FT_StreamRec_*, uint, byte*, uint, uint> Read;
        public delegate* unmanaged<FT_StreamRec_*, void> Close;
        public FT_MemoryRec_* Memory;
        public byte* Cursor;
        public byte* Limit;
    }

    internal unsafe struct FT_StreamDesc_
    {
        public int Value;
        public void* Pointer;
    }

    internal unsafe struct FT_Vector_
    {
        public int X;
        public int Y;
    }

    internal unsafe struct FT_BBox_
    {
        public int XMin;
        public int YMin;
        public int XMax;
        public int YMax;
    }

    internal unsafe struct FT_Bitmap_
    {
        public uint Rows;
        public uint Width;
        public int Pitch;
        public byte* Buffer;
        public ushort NumGrays;
        public byte PixelMode;
        public byte PaletteMode;
        public void* Palette;
    }

    internal unsafe struct FT_Outline_
    {
        public short NContours;
        public short NPoints;
        public FT_Vector_* Points;
        public sbyte* Tags;
        public short* Contours;
        public int Flags;
    }

    internal unsafe struct FT_Outline_Funcs_
    {
        public delegate* unmanaged<FT_Vector_*, void*, int> MoveTo;
        public delegate* unmanaged<FT_Vector_*, void*, int> LineTo;
        public delegate* unmanaged<FT_Vector_*, FT_Vector_*, void*, int> ConicTo;
        public delegate* unmanaged<FT_Vector_*, FT_Vector_*, FT_Vector_*, void*, int> CubicTo;
        public int Shift;
        public int Delta;
    }

    internal unsafe struct FT_Span_
    {
        public short X;
        public ushort Len;
        public byte Coverage;
    }

    internal unsafe struct FT_Raster_Params_
    {
        public FT_Bitmap_* Target;
        public void* Source;
        public int Flags;
        public delegate* unmanaged<int, int, FT_Span_*, void*, void> GraySpans;
        public delegate* unmanaged<int, int, FT_Span_*, void*, void> BlackSpans;
        public delegate* unmanaged<int, int, void*, int> BitTest;
        public delegate* unmanaged<int, int, void*, void> BitSet;
        public void* User;
        public FT_BBox_ ClipBox;
    }

    internal unsafe struct FT_Raster_Funcs_
    {
        public int GlyphFormat;
        public delegate* unmanaged<void*, IntPtr, int> RasterNew;
        public delegate* unmanaged<IntPtr, byte*, uint, void> RasterReset;
        public delegate* unmanaged<IntPtr, uint, void*, int> RasterSetMode;
        public delegate* unmanaged<IntPtr, FT_Raster_Params_*, int> RasterRender;
        public delegate* unmanaged<IntPtr, void> RasterDone;
    }

    internal unsafe struct FT_UnitVector_
    {
        public short X;
        public short Y;
    }

    internal unsafe struct FT_Matrix_
    {
        public int Xx;
        public int Xy;
        public int Yx;
        public int Yy;
    }

    internal unsafe struct FT_Data_
    {
        public byte* Pointer;
        public int Length;
    }

    internal unsafe struct FT_Generic_
    {
        public void* Data;
        public delegate* unmanaged<void*, void> Finalizer;
    }

    internal unsafe struct FT_ListNodeRec_
    {
        public FT_ListNodeRec_* Prev;
        public FT_ListNodeRec_* Next;
        public void* Data;
    }

    internal unsafe struct FT_ListRec_
    {
        public FT_ListNodeRec_* Head;
        public FT_ListNodeRec_* Tail;
    }

    internal unsafe struct FT_Glyph_Metrics_
    {
        public int Width;
        public int Height;
        public int HoriBearingX;
        public int HoriBearingY;
        public int HoriAdvance;
        public int VertBearingX;
        public int VertBearingY;
        public int VertAdvance;
    }

    internal unsafe struct FT_Bitmap_Size_
    {
        public short Height;
        public short Width;
        public int Size;
        public int XPpem;
        public int YPpem;
    }

    internal unsafe struct FT_FaceRec_
    {
        public int NumFaces;
        public int FaceIndex;
        public int FaceFlags;
        public int StyleFlags;
        public int NumGlyphs;
        public sbyte* FamilyName;
        public sbyte* StyleName;
        public int NumFixedSizes;
        public FT_Bitmap_Size_* AvailableSizes;
        public int NumCharmaps;
        public FT_CharMapRec_** Charmaps;
        public FT_Generic_ Generic;
        public FT_BBox_ Bbox;
        public ushort UnitsPerEM;
        public short Ascender;
        public short Descender;
        public short Height;
        public short MaxAdvanceWidth;
        public short MaxAdvanceHeight;
        public short UnderlinePosition;
        public short UnderlineThickness;
        public FT_GlyphSlotRec_* Glyph;
        public FT_SizeRec_* Size;
        public FT_CharMapRec_* Charmap;
        public IntPtr Driver;
        public FT_MemoryRec_* Memory;
        public FT_StreamRec_* Stream;
        public FT_ListRec_ SizesList;
        public FT_Generic_ Autohint;
        public void* Extensions;
        public IntPtr Internal;
    }

    internal unsafe struct FT_CharMapRec_
    {
        public FT_FaceRec_* Face;
        public int Encoding;
        public ushort PlatformId;
        public ushort EncodingId;
    }

    internal unsafe struct FT_GlyphSlotRec_
    {
        public IntPtr Library;
        public FT_FaceRec_* Face;
        public FT_GlyphSlotRec_* Next;
        public uint GlyphIndex;
        public FT_Generic_ Generic;
        public FT_Glyph_Metrics_ Metrics;
        public int LinearHoriAdvance;
        public int LinearVertAdvance;
        public FT_Vector_ Advance;
        public int Format;
        public FT_Bitmap_ Bitmap;
        public int BitmapLeft;
        public int BitmapTop;
        public FT_Outline_ Outline;
        public uint NumSubglyphs;
        public IntPtr Subglyphs;
        public void* ControlData;
        public int ControlLen;
        public int LsbDelta;
        public int RsbDelta;
        public void* Other;
        public IntPtr Internal;
    }

    internal unsafe struct FT_SizeRec_
    {
        public FT_FaceRec_* Face;
        public FT_Generic_ Generic;
        public FT_Size_Metrics_ Metrics;
        public IntPtr Internal;
    }

    internal unsafe struct FT_Size_Metrics_
    {
        public ushort XPpem;
        public ushort YPpem;
        public int XScale;
        public int YScale;
        public int Ascender;
        public int Descender;
        public int Height;
        public int MaxAdvance;
    }

    internal unsafe struct FT_Parameter_
    {
        public uint Tag;
        public void* Data;
    }

    internal unsafe struct FT_Open_Args_
    {
        public uint Flags;
        public byte* MemoryBase;
        public int MemorySize;
        public sbyte* Pathname;
        public FT_StreamRec_* Stream;
        public IntPtr Driver;
        public int NumParams;
        public FT_Parameter_* Params;
    }

    internal unsafe struct FT_Size_RequestRec_
    {
        public int Type;
        public int Width;
        public int Height;
        public uint HoriResolution;
        public uint VertResolution;
    }

    internal unsafe struct FT_LayerIterator_
    {
        public uint NumLayers;
        public uint Layer;
        public byte* P;
    }

    internal unsafe struct FT_GlyphRec_
    {
        public IntPtr Library;
        public IntPtr Clazz;
        public int Format;
        public FT_Vector_ Advance;
    }

    internal unsafe struct FT_BitmapGlyphRec_
    {
        public FT_GlyphRec_ Root;
        public int Left;
        public int Top;
        public FT_Bitmap_ Bitmap;
    }

    internal unsafe struct FT_OutlineGlyphRec_
    {
        public FT_GlyphRec_ Root;
        public FT_Outline_ Outline;
    }

    internal unsafe struct FT_Color_
    {
        public byte Blue;
        public byte Green;
        public byte Red;
        public byte Alpha;
    }

    internal unsafe struct FT_Palette_Data_
    {
        public ushort NumPalettes;
        public ushort* PaletteNameIds;
        public ushort* PaletteFlags;
        public ushort NumPaletteEntries;
        public ushort* PaletteEntryNameIds;
    }

    //work as a singletone
    //functions from DLL require the initialized library object,so it is initialized in the static class constructor
    //All calls that require library object must use "instance" as it
    internal unsafe static class Ft
    {
        public static IntPtr Instance;
        static Ft()
        {
            Win32NativeLoader loader = new Win32NativeLoader();
            IntPtr lib = loader.LoadLibrary("freetype.dll");
            LoadAll(sym => loader.GetProcAddress(lib, sym));

            Init();
        }

        private static void Init()
        {
            fixed (IntPtr* instancePtr = &Instance)
                InitFreeType((IntPtr)instancePtr);
        }

        public const int RENDER_POOL_SIZE = 16384;
        public const int MAX_MODULES = 32;
        public const int TT_CONFIG_OPTION_SUBPIXEL_HINTING = 2;
        public const int TT_CONFIG_OPTION_MAX_RUNNABLE_OPCODES = 1000000;
        public const int T1_MAX_DICT_DEPTH = 5;
        public const int T1_MAX_SUBRS_CALLS = 16;
        public const int T1_MAX_CHARSTRINGS_OPERANDS = 256;
        public const int CFF_CONFIG_OPTION_DARKENING_PARAMETER_X1 = 500;
        public const int CFF_CONFIG_OPTION_DARKENING_PARAMETER_Y1 = 400;
        public const int CFF_CONFIG_OPTION_DARKENING_PARAMETER_X2 = 1000;
        public const int CFF_CONFIG_OPTION_DARKENING_PARAMETER_Y2 = 275;
        public const int CFF_CONFIG_OPTION_DARKENING_PARAMETER_X3 = 1667;
        public const int CFF_CONFIG_OPTION_DARKENING_PARAMETER_Y3 = 275;
        public const int CFF_CONFIG_OPTION_DARKENING_PARAMETER_X4 = 2333;
        public const int CFF_CONFIG_OPTION_DARKENING_PARAMETER_Y4 = 0;
        public const int OUTLINE_NONE = 0x0;
        public const int OUTLINE_OWNER = 0x1;
        public const int OUTLINE_EVEN_ODD_FILL = 0x2;
        public const int OUTLINE_REVERSE_FILL = 0x4;
        public const int OUTLINE_IGNORE_DROPOUTS = 0x8;
        public const int OUTLINE_SMART_DROPOUTS = 0x10;
        public const int OUTLINE_INCLUDE_STUBS = 0x20;
        public const int OUTLINE_HIGH_PRECISION = 0x100;
        public const int OUTLINE_SINGLE_PASS = 0x200;
        public const int CURVE_TAG_ON = 0x01;
        public const int CURVE_TAG_CONIC = 0x00;
        public const int CURVE_TAG_CUBIC = 0x02;
        public const int CURVE_TAG_HAS_SCANMODE = 0x04;
        public const int CURVE_TAG_TOUCH_X = 0x08;
        public const int CURVE_TAG_TOUCH_Y = 0x10;
        public const int CURVE_TAG_TOUCH_BOTH = (CURVE_TAG_TOUCH_X | CURVE_TAG_TOUCH_Y);
        public const int RASTER_FLAG_DEFAULT = 0x0;
        public const int RASTER_FLAG_AA = 0x1;
        public const int RASTER_FLAG_DIRECT = 0x2;
        public const int RASTER_FLAG_CLIP = 0x4;
        public const int ERR_BASE = 0;
        public const int FACE_FLAG_SCALABLE = (1 << 0);
        public const int FACE_FLAG_FIXED_SIZES = (1 << 1);
        public const int FACE_FLAG_FIXED_WIDTH = (1 << 2);
        public const int FACE_FLAG_SFNT = (1 << 3);
        public const int FACE_FLAG_HORIZONTAL = (1 << 4);
        public const int FACE_FLAG_VERTICAL = (1 << 5);
        public const int FACE_FLAG_KERNING = (1 << 6);
        public const int FACE_FLAG_FAST_GLYPHS = (1 << 7);
        public const int FACE_FLAG_MULTIPLE_MASTERS = (1 << 8);
        public const int FACE_FLAG_GLYPH_NAMES = (1 << 9);
        public const int FACE_FLAG_EXTERNAL_STREAM = (1 << 10);
        public const int FACE_FLAG_HINTER = (1 << 11);
        public const int FACE_FLAG_CID_KEYED = (1 << 12);
        public const int FACE_FLAG_TRICKY = (1 << 13);
        public const int FACE_FLAG_COLOR = (1 << 14);
        public const int FACE_FLAG_VARIATION = (1 << 15);
        public const int HAS_FAST_GLYPHS = 0;
        public const int STYLE_FLAG_ITALIC = (1 << 0);
        public const int STYLE_FLAG_BOLD = (1 << 1);
        public const int OPEN_MEMORY = 0x1;
        public const int OPEN_STREAM = 0x2;
        public const int OPEN_PATHNAME = 0x4;
        public const int OPEN_DRIVER = 0x8;
        public const int OPEN_PARAMS = 0x10;
        public const int LOAD_DEFAULT = 0x0;
        public const int LOAD_NO_SCALE = (1 << 0);
        public const int LOAD_NO_HINTING = (1 << 1);
        public const int LOAD_RENDER = (1 << 2);
        public const int LOAD_NO_BITMAP = (1 << 3);
        public const int LOAD_VERTICAL_LAYOUT = (1 << 4);
        public const int LOAD_FORCE_AUTOHINT = (1 << 5);
        public const int LOAD_CROP_BITMAP = (1 << 6);
        public const int LOAD_PEDANTIC = (1 << 7);
        public const int LOAD_IGNORE_GLOBAL_ADVANCE_WIDTH = (1 << 9);
        public const int LOAD_NO_RECURSE = (1 << 10);
        public const int LOAD_IGNORE_TRANSFORM = (1 << 11);
        public const int LOAD_MONOCHROME = (1 << 12);
        public const int LOAD_LINEAR_DESIGN = (1 << 13);
        public const int LOAD_NO_AUTOHINT = (1 << 15);
        public const int LOAD_COLOR = (1 << 20);
        public const int LOAD_COMPUTE_METRICS = (1 << 21);
        public const int LOAD_BITMAP_METRICS_ONLY = (1 << 22);
        public const int LOAD_ADVANCE_ONLY = (1 << 8);
        public const int LOAD_SBITS_ONLY = (1 << 14);
        public const int SUBGLYPH_FLAG_ARGS_ARE_WORDS = 1;
        public const int SUBGLYPH_FLAG_ARGS_ARE_XY_VALUES = 2;
        public const int SUBGLYPH_FLAG_ROUND_XY_TO_GRID = 4;
        public const int SUBGLYPH_FLAG_SCALE = 8;
        public const int SUBGLYPH_FLAG_XY_SCALE = 0x40;
        public const int SUBGLYPH_FLAG_2X2 = 0x80;
        public const int SUBGLYPH_FLAG_USE_MY_METRICS = 0x200;
        public const int FSTYPE_INSTALLABLE_EMBEDDING = 0x0000;
        public const int FSTYPE_RESTRICTED_LICENSE_EMBEDDING = 0x0002;
        public const int FSTYPE_PREVIEW_AND_PRINT_EMBEDDING = 0x0004;
        public const int FSTYPE_EDITABLE_EMBEDDING = 0x0008;
        public const int FSTYPE_NO_SUBSETTING = 0x0100;
        public const int FSTYPE_BITMAP_EMBEDDING_ONLY = 0x0200;
        public const int FREETYPE_MAJOR = 2;
        public const int FREETYPE_MINOR = 10;
        public const int FREETYPE_PATCH = 1;
        public const int PALETTE_FOR_LIGHT_BACKGROUND = 0x01;
        public const int PALETTE_FOR_DARK_BACKGROUND = 0x02;
        public const int PIXEL_MODE_NONE = 0;
        public const int PIXEL_MODE_MONO = 1;
        public const int PIXEL_MODE_GRAY = 2;
        public const int PIXEL_MODE_GRAY2 = 3;
        public const int PIXEL_MODE_GRAY4 = 4;
        public const int PIXEL_MODE_LCD = 5;
        public const int PIXEL_MODE_LCD_V = 6;
        public const int PIXEL_MODE_BGRA = 7;
        public const int PIXEL_MODE_MAX = 8;
        public const int GLYPH_FORMAT_NONE = 0;
        public const int GLYPH_FORMAT_COMPOSITE = 1668246896;
        public const int GLYPH_FORMAT_BITMAP = 1651078259;
        public const int GLYPH_FORMAT_OUTLINE = 1869968492;
        public const int GLYPH_FORMAT_PLOTTER = 1886154612;
        public const int MOD_ERR_BASE = 0;
        public const int MOD_ERR_AUTOFIT = 0;
        public const int MOD_ERR_BDF = 0;
        public const int MOD_ERR_BZIP2 = 0;
        public const int MOD_ERR_CACHE = 0;
        public const int MOD_ERR_CFF = 0;
        public const int MOD_ERR_CID = 0;
        public const int MOD_ERR_GZIP = 0;
        public const int MOD_ERR_LZW = 0;
        public const int MOD_ERR_OTVALID = 0;
        public const int MOD_ERR_PCF = 0;
        public const int MOD_ERR_PFR = 0;
        public const int MOD_ERR_PSAUX = 0;
        public const int MOD_ERR_PSHINTER = 0;
        public const int MOD_ERR_PSNAMES = 0;
        public const int MOD_ERR_RASTER = 0;
        public const int MOD_ERR_SFNT = 0;
        public const int MOD_ERR_SMOOTH = 0;
        public const int MOD_ERR_TRUETYPE = 0;
        public const int MOD_ERR_TYPE1 = 0;
        public const int MOD_ERR_TYPE42 = 0;
        public const int MOD_ERR_WINFONTS = 0;
        public const int MOD_ERR_GXVALID = 0;
        public const int MOD_ERR_MAX = 1;
        public const int ERR_OK = 0;
        public const int ERR_CANNOT_OPEN_RESOURCE = 1;
        public const int ERR_UNKNOWN_FILE_FORMAT = 2;
        public const int ERR_INVALID_FILE_FORMAT = 3;
        public const int ERR_INVALID_VERSION = 4;
        public const int ERR_LOWER_MODULE_VERSION = 5;
        public const int ERR_INVALID_ARGUMENT = 6;
        public const int ERR_UNIMPLEMENTED_FEATURE = 7;
        public const int ERR_INVALID_TABLE = 8;
        public const int ERR_INVALID_OFFSET = 9;
        public const int ERR_ARRAY_TOO_LARGE = 10;
        public const int ERR_MISSING_MODULE = 11;
        public const int ERR_MISSING_PROPERTY = 12;
        public const int ERR_INVALID_GLYPH_INDEX = 16;
        public const int ERR_INVALID_CHARACTER_CODE = 17;
        public const int ERR_INVALID_GLYPH_FORMAT = 18;
        public const int ERR_CANNOT_RENDER_GLYPH = 19;
        public const int ERR_INVALID_OUTLINE = 20;
        public const int ERR_INVALID_COMPOSITE = 21;
        public const int ERR_TOO_MANY_HINTS = 22;
        public const int ERR_INVALID_PIXEL_SIZE = 23;
        public const int ERR_INVALID_HANDLE = 32;
        public const int ERR_INVALID_LIBRARY_HANDLE = 33;
        public const int ERR_INVALID_DRIVER_HANDLE = 34;
        public const int ERR_INVALID_FACE_HANDLE = 35;
        public const int ERR_INVALID_SIZE_HANDLE = 36;
        public const int ERR_INVALID_SLOT_HANDLE = 37;
        public const int ERR_INVALID_CHARMAP_HANDLE = 38;
        public const int ERR_INVALID_CACHE_HANDLE = 39;
        public const int ERR_INVALID_STREAM_HANDLE = 40;
        public const int ERR_TOO_MANY_DRIVERS = 48;
        public const int ERR_TOO_MANY_EXTENSIONS = 49;
        public const int ERR_OUT_OF_MEMORY = 64;
        public const int ERR_UNLISTED_OBJECT = 65;
        public const int ERR_CANNOT_OPEN_STREAM = 81;
        public const int ERR_INVALID_STREAM_SEEK = 82;
        public const int ERR_INVALID_STREAM_SKIP = 83;
        public const int ERR_INVALID_STREAM_READ = 84;
        public const int ERR_INVALID_STREAM_OPERATION = 85;
        public const int ERR_INVALID_FRAME_OPERATION = 86;
        public const int ERR_NESTED_FRAME_ACCESS = 87;
        public const int ERR_INVALID_FRAME_READ = 88;
        public const int ERR_RASTER_UNINITIALIZED = 96;
        public const int ERR_RASTER_CORRUPTED = 97;
        public const int ERR_RASTER_OVERFLOW = 98;
        public const int ERR_RASTER_NEGATIVE_HEIGHT = 99;
        public const int ERR_TOO_MANY_CACHES = 112;
        public const int ERR_INVALID_OPCODE = 128;
        public const int ERR_TOO_FEW_ARGUMENTS = 129;
        public const int ERR_STACK_OVERFLOW = 130;
        public const int ERR_CODE_OVERFLOW = 131;
        public const int ERR_BAD_ARGUMENT = 132;
        public const int ERR_DIVIDE_BY_ZERO = 133;
        public const int ERR_INVALID_REFERENCE = 134;
        public const int ERR_DEBUG_OPCODE = 135;
        public const int ERR_ENDF_IN_EXEC_STREAM = 136;
        public const int ERR_NESTED_DEFS = 137;
        public const int ERR_INVALID_CODERANGE = 138;
        public const int ERR_EXECUTION_TOO_LONG = 139;
        public const int ERR_TOO_MANY_FUNCTION_DEFS = 140;
        public const int ERR_TOO_MANY_INSTRUCTION_DEFS = 141;
        public const int ERR_TABLE_MISSING = 142;
        public const int ERR_HORIZ_HEADER_MISSING = 143;
        public const int ERR_LOCATIONS_MISSING = 144;
        public const int ERR_NAME_TABLE_MISSING = 145;
        public const int ERR_CMAP_TABLE_MISSING = 146;
        public const int ERR_HMTX_TABLE_MISSING = 147;
        public const int ERR_POST_TABLE_MISSING = 148;
        public const int ERR_INVALID_HORIZ_METRICS = 149;
        public const int ERR_INVALID_CHARMAP_FORMAT = 150;
        public const int ERR_INVALID_PPEM = 151;
        public const int ERR_INVALID_VERT_METRICS = 152;
        public const int ERR_COULD_NOT_FIND_CONTEXT = 153;
        public const int ERR_INVALID_POST_TABLE_FORMAT = 154;
        public const int ERR_INVALID_POST_TABLE = 155;
        public const int ERR_DEF_IN_GLYF_BYTECODE = 156;
        public const int ERR_MISSING_BITMAP = 157;
        public const int ERR_SYNTAX_ERROR = 160;
        public const int ERR_STACK_UNDERFLOW = 161;
        public const int ERR_IGNORE = 162;
        public const int ERR_NO_UNICODE_GLYPH_NAME = 163;
        public const int ERR_GLYPH_TOO_BIG = 164;
        public const int ERR_MISSING_STARTFONT_FIELD = 176;
        public const int ERR_MISSING_FONT_FIELD = 177;
        public const int ERR_MISSING_SIZE_FIELD = 178;
        public const int ERR_MISSING_FONTBOUNDINGBOX_FIELD = 179;
        public const int ERR_MISSING_CHARS_FIELD = 180;
        public const int ERR_MISSING_STARTCHAR_FIELD = 181;
        public const int ERR_MISSING_ENCODING_FIELD = 182;
        public const int ERR_MISSING_BBX_FIELD = 183;
        public const int ERR_BBX_TOO_BIG = 184;
        public const int ERR_CORRUPTED_FONT_HEADER = 185;
        public const int ERR_CORRUPTED_FONT_GLYPHS = 186;
        public const int ERR_MAX = 187;
        public const int ENCODING_NONE = 0;
        public const int ENCODING_MS_SYMBOL = 1937337698;
        public const int ENCODING_UNICODE = 1970170211;
        public const int ENCODING_SJIS = 1936353651;
        public const int ENCODING_PRC = 1734484000;
        public const int ENCODING_BIG5 = 1651074869;
        public const int ENCODING_WANSUNG = 2002873971;
        public const int ENCODING_JOHAB = 1785686113;
        public const int ENCODING_GB2312 = 1734484000;
        public const int ENCODING_MS_SJIS = 1936353651;
        public const int ENCODING_MS_GB2312 = 1734484000;
        public const int ENCODING_MS_BIG5 = 1651074869;
        public const int ENCODING_MS_WANSUNG = 2002873971;
        public const int ENCODING_MS_JOHAB = 1785686113;
        public const int ENCODING_ADOBE_STANDARD = 1094995778;
        public const int ENCODING_ADOBE_EXPERT = 1094992453;
        public const int ENCODING_ADOBE_CUSTOM = 1094992451;
        public const int ENCODING_ADOBE_LATIN_1 = 1818326065;
        public const int ENCODING_OLD_LATIN_2 = 1818326066;
        public const int ENCODING_APPLE_ROMAN = 1634889070;
        public const int SIZE_REQUEST_TYPE_NOMINAL = 0;
        public const int SIZE_REQUEST_TYPE_REAL_DIM = 1;
        public const int SIZE_REQUEST_TYPE_BBOX = 2;
        public const int SIZE_REQUEST_TYPE_CELL = 3;
        public const int SIZE_REQUEST_TYPE_SCALES = 4;
        public const int SIZE_REQUEST_TYPE_MAX = 5;
        public const int RENDER_MODE_NORMAL = 0;
        public const int RENDER_MODE_LIGHT = 1;
        public const int RENDER_MODE_MONO = 2;
        public const int RENDER_MODE_LCD = 3;
        public const int RENDER_MODE_LCD_V = 4;
        public const int RENDER_MODE_MAX = 5;
        public const int KERNING_DEFAULT = 0;
        public const int KERNING_UNFITTED = 1;
        public const int KERNING_UNSCALED = 2;
        public const int GLYPH_BBOX_UNSCALED = 0;
        public const int GLYPH_BBOX_SUBPIXELS = 0;
        public const int GLYPH_BBOX_GRIDFIT = 1;
        public const int GLYPH_BBOX_TRUNCATE = 2;
        public const int GLYPH_BBOX_PIXELS = 3;
        public const int ORIENTATION_TRUETYPE = 0;
        public const int ORIENTATION_POSTSCRIPT = 1;
        public const int ORIENTATION_FILL_RIGHT = 0;
        public const int ORIENTATION_FILL_LEFT = 1;
        public const int ORIENTATION_NONE = 2;
        public const int STROKER_LINEJOIN_ROUND = 0;
        public const int STROKER_LINEJOIN_BEVEL = 1;
        public const int STROKER_LINEJOIN_MITER_VARIABLE = 2;
        public const int STROKER_LINEJOIN_MITER = 2;
        public const int STROKER_LINEJOIN_MITER_FIXED = 3;
        public const int STROKER_LINECAP_BUTT = 0;
        public const int STROKER_LINECAP_ROUND = 1;
        public const int STROKER_LINECAP_SQUARE = 2;
        public const int STROKER_BORDER_LEFT = 0;
        public const int STROKER_BORDER_RIGHT = 1;

        private static delegate* unmanaged<int, sbyte*> _fnErrorString;
        public static sbyte* ErrorString(int errorCode) => _fnErrorString(errorCode);
        private static delegate* unmanaged<IntPtr, int> _fnInitFreeType;
        public static int InitFreeType(IntPtr alibrary) => _fnInitFreeType(alibrary);
        private static delegate* unmanaged<IntPtr, int> _fnDoneFreeType;
        public static int DoneFreeType(IntPtr library) => _fnDoneFreeType(library);
        private static delegate* unmanaged<IntPtr, sbyte*, int, FT_FaceRec_**, int> _fnNewFace;
        public static int NewFace(IntPtr library, sbyte* filepathname, int faceIndex, FT_FaceRec_** aface) => _fnNewFace(library, filepathname, faceIndex, aface);
        private static delegate* unmanaged<IntPtr, byte*, int, int, FT_FaceRec_**, int> _fnNewMemoryFace;
        public static int NewMemoryFace(IntPtr library, byte* fileBase, int fileSize, int faceIndex, FT_FaceRec_** aface) => _fnNewMemoryFace(library, fileBase, fileSize, faceIndex, aface);
        private static delegate* unmanaged<IntPtr, FT_Open_Args_*, int, FT_FaceRec_**, int> _fnOpenFace;
        public static int OpenFace(IntPtr library, FT_Open_Args_* args, int faceIndex, FT_FaceRec_** aface) => _fnOpenFace(library, args, faceIndex, aface);
        private static delegate* unmanaged<FT_FaceRec_*, sbyte*, int> _fnAttachFile;
        public static int AttachFile(FT_FaceRec_* face, sbyte* filepathname) => _fnAttachFile(face, filepathname);
        private static delegate* unmanaged<FT_FaceRec_*, FT_Open_Args_*, int> _fnAttachStream;
        public static int AttachStream(FT_FaceRec_* face, FT_Open_Args_* parameters) => _fnAttachStream(face, parameters);
        private static delegate* unmanaged<FT_FaceRec_*, int> _fnReferenceFace;
        public static int ReferenceFace(FT_FaceRec_* face) => _fnReferenceFace(face);
        private static delegate* unmanaged<FT_FaceRec_*, int> _fnDoneFace;
        public static int DoneFace(FT_FaceRec_* face) => _fnDoneFace(face);
        private static delegate* unmanaged<FT_FaceRec_*, int, int> _fnSelectSize;
        public static int SelectSize(FT_FaceRec_* face, int strikeIndex) => _fnSelectSize(face, strikeIndex);
        private static delegate* unmanaged<FT_FaceRec_*, FT_Size_RequestRec_*, int> _fnRequestSize;
        public static int RequestSize(FT_FaceRec_* face, FT_Size_RequestRec_* req) => _fnRequestSize(face, req);
        private static delegate* unmanaged<FT_FaceRec_*, int, int, uint, uint, int> _fnSetCharSize;
        public static int SetCharSize(FT_FaceRec_* face, int charWidth, int charHeight, uint horzResolution, uint vertResolution) => _fnSetCharSize(face, charWidth, charHeight, horzResolution, vertResolution);
        private static delegate* unmanaged<FT_FaceRec_*, uint, uint, int> _fnSetPixelSizes;
        public static int SetPixelSizes(FT_FaceRec_* face, uint pixelWidth, uint pixelHeight) => _fnSetPixelSizes(face, pixelWidth, pixelHeight);
        private static delegate* unmanaged<FT_FaceRec_*, uint, int, int> _fnLoadGlyph;
        public static int LoadGlyph(FT_FaceRec_* face, uint glyphIndex, int loadFlags) => _fnLoadGlyph(face, glyphIndex, loadFlags);
        private static delegate* unmanaged<FT_FaceRec_*, uint, int, int> _fnLoadChar;
        public static int LoadChar(FT_FaceRec_* face, uint charCode, int loadFlags) => _fnLoadChar(face, charCode, loadFlags);
        private static delegate* unmanaged<FT_FaceRec_*, FT_Matrix_*, FT_Vector_*, void> _fnSetTransform;
        public static void SetTransform(FT_FaceRec_* face, FT_Matrix_* matrix, FT_Vector_* delta) => _fnSetTransform(face, matrix, delta);
        private static delegate* unmanaged<FT_GlyphSlotRec_*, int, int> _fnRenderGlyph;
        public static int RenderGlyph(FT_GlyphSlotRec_* slot, int renderMode) => _fnRenderGlyph(slot, renderMode);
        private static delegate* unmanaged<FT_FaceRec_*, uint, uint, uint, FT_Vector_*, int> _fnGetKerning;
        public static int GetKerning(FT_FaceRec_* face, uint leftGlyph, uint rightGlyph, uint kernMode, FT_Vector_* akerning) => _fnGetKerning(face, leftGlyph, rightGlyph, kernMode, akerning);
        private static delegate* unmanaged<FT_FaceRec_*, int, int, int*, int> _fnGetTrackKerning;
        public static int GetTrackKerning(FT_FaceRec_* face, int pointSize, int degree, int* akerning) => _fnGetTrackKerning(face, pointSize, degree, akerning);
        private static delegate* unmanaged<FT_FaceRec_*, uint, void*, uint, int> _fnGetGlyphName;
        public static int GetGlyphName(FT_FaceRec_* face, uint glyphIndex, void* buffer, uint bufferMax) => _fnGetGlyphName(face, glyphIndex, buffer, bufferMax);
        private static delegate* unmanaged<FT_FaceRec_*, sbyte*> _fnGetPostscriptName;
        public static sbyte* GetPostscriptName(FT_FaceRec_* face) => _fnGetPostscriptName(face);
        private static delegate* unmanaged<FT_FaceRec_*, int, int> _fnSelectCharmap;
        public static int SelectCharmap(FT_FaceRec_* face, int encoding) => _fnSelectCharmap(face, encoding);
        private static delegate* unmanaged<FT_FaceRec_*, FT_CharMapRec_*, int> _fnSetCharmap;
        public static int SetCharmap(FT_FaceRec_* face, FT_CharMapRec_* charmap) => _fnSetCharmap(face, charmap);
        private static delegate* unmanaged<FT_CharMapRec_*, int> _fnGetCharmapIndex;
        public static int GetCharmapIndex(FT_CharMapRec_* charmap) => _fnGetCharmapIndex(charmap);
        private static delegate* unmanaged<FT_FaceRec_*, uint, uint> _fnGetCharIndex;
        public static uint GetCharIndex(FT_FaceRec_* face, uint charcode) => _fnGetCharIndex(face, charcode);
        private static delegate* unmanaged<FT_FaceRec_*, uint*, uint> _fnGetFirstChar;
        public static uint GetFirstChar(FT_FaceRec_* face, uint* agindex) => _fnGetFirstChar(face, agindex);
        private static delegate* unmanaged<FT_FaceRec_*, uint, uint*, uint> _fnGetNextChar;
        public static uint GetNextChar(FT_FaceRec_* face, uint charCode, uint* agindex) => _fnGetNextChar(face, charCode, agindex);
        private static delegate* unmanaged<FT_FaceRec_*, uint, FT_Parameter_*, int> _fnFaceProperties;
        public static int FaceProperties(FT_FaceRec_* face, uint numProperties, FT_Parameter_* properties) => _fnFaceProperties(face, numProperties, properties);
        private static delegate* unmanaged<FT_FaceRec_*, sbyte*, uint> _fnGetNameIndex;
        public static uint GetNameIndex(FT_FaceRec_* face, sbyte* glyphName) => _fnGetNameIndex(face, glyphName);
        private static delegate* unmanaged<FT_GlyphSlotRec_*, uint, int*, uint*, int*, int*, FT_Matrix_*, int> _fnGetSubGlyphInfo;
        public static int GetSubGlyphInfo(FT_GlyphSlotRec_* glyph, uint subIndex, int* pIndex, uint* pFlags, int* pArg1, int* pArg2, FT_Matrix_* pTransform) => _fnGetSubGlyphInfo(glyph, subIndex, pIndex, pFlags, pArg1, pArg2, pTransform);
        private static delegate* unmanaged<FT_FaceRec_*, uint, uint*, uint*, FT_LayerIterator_*, byte> _fnGetColorGlyphLayer;
        public static byte GetColorGlyphLayer(FT_FaceRec_* face, uint baseGlyph, uint* aglyphIndex, uint* acolorIndex, FT_LayerIterator_* iterator) => _fnGetColorGlyphLayer(face, baseGlyph, aglyphIndex, acolorIndex, iterator);
        private static delegate* unmanaged<FT_FaceRec_*, ushort> _fnGetFSTypeFlags;
        public static ushort GetFSTypeFlags(FT_FaceRec_* face) => _fnGetFSTypeFlags(face);
        private static delegate* unmanaged<FT_FaceRec_*, uint, uint, uint> _fnFaceGetCharVariantIndex;
        public static uint FaceGetCharVariantIndex(FT_FaceRec_* face, uint charcode, uint variantSelector) => _fnFaceGetCharVariantIndex(face, charcode, variantSelector);
        private static delegate* unmanaged<FT_FaceRec_*, uint, uint, int> _fnFaceGetCharVariantIsDefault;
        public static int FaceGetCharVariantIsDefault(FT_FaceRec_* face, uint charcode, uint variantSelector) => _fnFaceGetCharVariantIsDefault(face, charcode, variantSelector);
        private static delegate* unmanaged<FT_FaceRec_*, uint*> _fnFaceGetVariantSelectors;
        public static uint* FaceGetVariantSelectors(FT_FaceRec_* face) => _fnFaceGetVariantSelectors(face);
        private static delegate* unmanaged<FT_FaceRec_*, uint, uint*> _fnFaceGetVariantsOfChar;
        public static uint* FaceGetVariantsOfChar(FT_FaceRec_* face, uint charcode) => _fnFaceGetVariantsOfChar(face, charcode);
        private static delegate* unmanaged<FT_FaceRec_*, uint, uint*> _fnFaceGetCharsOfVariant;
        public static uint* FaceGetCharsOfVariant(FT_FaceRec_* face, uint variantSelector) => _fnFaceGetCharsOfVariant(face, variantSelector);
        private static delegate* unmanaged<int, int, int, int> _fnMulDiv;
        public static int MulDiv(int a, int b, int c) => _fnMulDiv(a, b, c);
        private static delegate* unmanaged<int, int, int> _fnMulFix;
        public static int MulFix(int a, int b) => _fnMulFix(a, b);
        private static delegate* unmanaged<int, int, int> _fnDivFix;
        public static int DivFix(int a, int b) => _fnDivFix(a, b);
        private static delegate* unmanaged<int, int> _fnRoundFix;
        public static int RoundFix(int a) => _fnRoundFix(a);
        private static delegate* unmanaged<int, int> _fnCeilFix;
        public static int CeilFix(int a) => _fnCeilFix(a);
        private static delegate* unmanaged<int, int> _fnFloorFix;
        public static int FloorFix(int a) => _fnFloorFix(a);
        private static delegate* unmanaged<FT_Vector_*, FT_Matrix_*, void> _fnVectorTransform;
        public static void VectorTransform(FT_Vector_* vector, FT_Matrix_* matrix) => _fnVectorTransform(vector, matrix);
        private static delegate* unmanaged<IntPtr, int*, int*, int*, void> _fnLibraryVersion;
        public static void LibraryVersion(IntPtr library, int* amajor, int* aminor, int* apatch) => _fnLibraryVersion(library, amajor, aminor, apatch);
        private static delegate* unmanaged<FT_FaceRec_*, byte> _fnFaceCheckTrueTypePatents;
        public static byte FaceCheckTrueTypePatents(FT_FaceRec_* face) => _fnFaceCheckTrueTypePatents(face);
        private static delegate* unmanaged<FT_FaceRec_*, byte, byte> _fnFaceSetUnpatentedHinting;
        public static byte FaceSetUnpatentedHinting(FT_FaceRec_* face, byte value) => _fnFaceSetUnpatentedHinting(face, value);
        private static delegate* unmanaged<IntPtr, int, FT_GlyphRec_**, int> _fnNewGlyph;
        public static int NewGlyph(IntPtr library, int format, FT_GlyphRec_** aglyph) => _fnNewGlyph(library, format, aglyph);
        private static delegate* unmanaged<FT_GlyphSlotRec_*, FT_GlyphRec_**, int> _fnGetGlyph;
        public static int GetGlyph(FT_GlyphSlotRec_* slot, FT_GlyphRec_** aglyph) => _fnGetGlyph(slot, aglyph);
        private static delegate* unmanaged<FT_GlyphRec_*, FT_GlyphRec_**, int> _fnGlyphCopy;
        public static int GlyphCopy(FT_GlyphRec_* source, FT_GlyphRec_** target) => _fnGlyphCopy(source, target);
        private static delegate* unmanaged<FT_GlyphRec_*, FT_Matrix_*, FT_Vector_*, int> _fnGlyphTransform;
        public static int GlyphTransform(FT_GlyphRec_* glyph, FT_Matrix_* matrix, FT_Vector_* delta) => _fnGlyphTransform(glyph, matrix, delta);
        private static delegate* unmanaged<FT_GlyphRec_*, uint, FT_BBox_*, void> _fnGlyphGetCBox;
        public static void GlyphGetCBox(FT_GlyphRec_* glyph, uint bboxMode, FT_BBox_* acbox) => _fnGlyphGetCBox(glyph, bboxMode, acbox);
        private static delegate* unmanaged<FT_GlyphRec_**, int, FT_Vector_*, byte, int> _fnGlyphToBitmap;
        public static int GlyphToBitmap(FT_GlyphRec_** theGlyph, int renderMode, FT_Vector_* origin, byte destroy) => _fnGlyphToBitmap(theGlyph, renderMode, origin, destroy);
        private static delegate* unmanaged<FT_GlyphRec_*, void> _fnDoneGlyph;
        public static void DoneGlyph(FT_GlyphRec_* glyph) => _fnDoneGlyph(glyph);
        private static delegate* unmanaged<FT_Matrix_*, FT_Matrix_*, void> _fnMatrixMultiply;
        public static void MatrixMultiply(FT_Matrix_* a, FT_Matrix_* b) => _fnMatrixMultiply(a, b);
        private static delegate* unmanaged<FT_Matrix_*, int> _fnMatrixInvert;
        public static int MatrixInvert(FT_Matrix_* matrix) => _fnMatrixInvert(matrix);
        private static delegate* unmanaged<FT_Outline_*, FT_Outline_Funcs_*, void*, int> _fnOutlineDecompose;
        public static int OutlineDecompose(FT_Outline_* outline, FT_Outline_Funcs_* funcInterface, void* user) => _fnOutlineDecompose(outline, funcInterface, user);
        private static delegate* unmanaged<IntPtr, uint, int, FT_Outline_*, int> _fnOutlineNew;
        public static int OutlineNew(IntPtr library, uint numPoints, int numContours, FT_Outline_* anoutline) => _fnOutlineNew(library, numPoints, numContours, anoutline);
        private static delegate* unmanaged<IntPtr, FT_Outline_*, int> _fnOutlineDone;
        public static int OutlineDone(IntPtr library, FT_Outline_* outline) => _fnOutlineDone(library, outline);
        private static delegate* unmanaged<FT_Outline_*, int> _fnOutlineCheck;
        public static int OutlineCheck(FT_Outline_* outline) => _fnOutlineCheck(outline);
        private static delegate* unmanaged<FT_Outline_*, FT_BBox_*, void> _fnOutlineGetCBox;
        public static void OutlineGetCBox(FT_Outline_* outline, FT_BBox_* acbox) => _fnOutlineGetCBox(outline, acbox);
        private static delegate* unmanaged<FT_Outline_*, int, int, void> _fnOutlineTranslate;
        public static void OutlineTranslate(FT_Outline_* outline, int xOffset, int yOffset) => _fnOutlineTranslate(outline, xOffset, yOffset);
        private static delegate* unmanaged<FT_Outline_*, FT_Outline_*, int> _fnOutlineCopy;
        public static int OutlineCopy(FT_Outline_* source, FT_Outline_* target) => _fnOutlineCopy(source, target);
        private static delegate* unmanaged<FT_Outline_*, FT_Matrix_*, void> _fnOutlineTransform;
        public static void OutlineTransform(FT_Outline_* outline, FT_Matrix_* matrix) => _fnOutlineTransform(outline, matrix);
        private static delegate* unmanaged<FT_Outline_*, int, int> _fnOutlineEmbolden;
        public static int OutlineEmbolden(FT_Outline_* outline, int strength) => _fnOutlineEmbolden(outline, strength);
        private static delegate* unmanaged<FT_Outline_*, int, int, int> _fnOutlineEmboldenXY;
        public static int OutlineEmboldenXY(FT_Outline_* outline, int xstrength, int ystrength) => _fnOutlineEmboldenXY(outline, xstrength, ystrength);
        private static delegate* unmanaged<FT_Outline_*, void> _fnOutlineReverse;
        public static void OutlineReverse(FT_Outline_* outline) => _fnOutlineReverse(outline);
        private static delegate* unmanaged<IntPtr, FT_Outline_*, FT_Bitmap_*, int> _fnOutlineGetBitmap;
        public static int OutlineGetBitmap(IntPtr library, FT_Outline_* outline, FT_Bitmap_* abitmap) => _fnOutlineGetBitmap(library, outline, abitmap);
        private static delegate* unmanaged<IntPtr, FT_Outline_*, FT_Raster_Params_*, int> _fnOutlineRender;
        public static int OutlineRender(IntPtr library, FT_Outline_* outline, FT_Raster_Params_* @params) => _fnOutlineRender(library, outline, @params);
        private static delegate* unmanaged<FT_Outline_*, int> _fnOutlineGetOrientation;
        public static int OutlineGetOrientation(FT_Outline_* outline) => _fnOutlineGetOrientation(outline);
        private static delegate* unmanaged<FT_Outline_*, int> _fnOutlineGetInsideBorder;
        public static int OutlineGetInsideBorder(FT_Outline_* outline) => _fnOutlineGetInsideBorder(outline);
        private static delegate* unmanaged<FT_Outline_*, int> _fnOutlineGetOutsideBorder;
        public static int OutlineGetOutsideBorder(FT_Outline_* outline) => _fnOutlineGetOutsideBorder(outline);
        private static delegate* unmanaged<IntPtr, IntPtr, int> _fnStrokerNew;
        public static int StrokerNew(IntPtr library, IntPtr astroker) => _fnStrokerNew(library, astroker);
        private static delegate* unmanaged<IntPtr, int, int, int, int, void> _fnStrokerSet;
        public static void StrokerSet(IntPtr stroker, int radius, int lineCap, int lineJoin, int miterLimit) => _fnStrokerSet(stroker, radius, lineCap, lineJoin, miterLimit);
        private static delegate* unmanaged<IntPtr, void> _fnStrokerRewind;
        public static void StrokerRewind(IntPtr stroker) => _fnStrokerRewind(stroker);
        private static delegate* unmanaged<IntPtr, FT_Outline_*, byte, int> _fnStrokerParseOutline;
        public static int StrokerParseOutline(IntPtr stroker, FT_Outline_* outline, byte opened) => _fnStrokerParseOutline(stroker, outline, opened);
        private static delegate* unmanaged<IntPtr, FT_Vector_*, byte, int> _fnStrokerBeginSubPath;
        public static int StrokerBeginSubPath(IntPtr stroker, FT_Vector_* to, byte open) => _fnStrokerBeginSubPath(stroker, to, open);
        private static delegate* unmanaged<IntPtr, int> _fnStrokerEndSubPath;
        public static int StrokerEndSubPath(IntPtr stroker) => _fnStrokerEndSubPath(stroker);
        private static delegate* unmanaged<IntPtr, FT_Vector_*, int> _fnStrokerLineTo;
        public static int StrokerLineTo(IntPtr stroker, FT_Vector_* to) => _fnStrokerLineTo(stroker, to);
        private static delegate* unmanaged<IntPtr, FT_Vector_*, FT_Vector_*, int> _fnStrokerConicTo;
        public static int StrokerConicTo(IntPtr stroker, FT_Vector_* control, FT_Vector_* to) => _fnStrokerConicTo(stroker, control, to);
        private static delegate* unmanaged<IntPtr, FT_Vector_*, FT_Vector_*, FT_Vector_*, int> _fnStrokerCubicTo;
        public static int StrokerCubicTo(IntPtr stroker, FT_Vector_* control1, FT_Vector_* control2, FT_Vector_* to) => _fnStrokerCubicTo(stroker, control1, control2, to);
        private static delegate* unmanaged<IntPtr, int, uint*, uint*, int> _fnStrokerGetBorderCounts;
        public static int StrokerGetBorderCounts(IntPtr stroker, int border, uint* anumPoints, uint* anumContours) => _fnStrokerGetBorderCounts(stroker, border, anumPoints, anumContours);
        private static delegate* unmanaged<IntPtr, int, FT_Outline_*, void> _fnStrokerExportBorder;
        public static void StrokerExportBorder(IntPtr stroker, int border, FT_Outline_* outline) => _fnStrokerExportBorder(stroker, border, outline);
        private static delegate* unmanaged<IntPtr, uint*, uint*, int> _fnStrokerGetCounts;
        public static int StrokerGetCounts(IntPtr stroker, uint* anumPoints, uint* anumContours) => _fnStrokerGetCounts(stroker, anumPoints, anumContours);
        private static delegate* unmanaged<IntPtr, FT_Outline_*, void> _fnStrokerExport;
        public static void StrokerExport(IntPtr stroker, FT_Outline_* outline) => _fnStrokerExport(stroker, outline);
        private static delegate* unmanaged<IntPtr, void> _fnStrokerDone;
        public static void StrokerDone(IntPtr stroker) => _fnStrokerDone(stroker);
        private static delegate* unmanaged<FT_GlyphRec_**, IntPtr, byte, int> _fnGlyphStroke;
        public static int GlyphStroke(FT_GlyphRec_** pglyph, IntPtr stroker, byte destroy) => _fnGlyphStroke(pglyph, stroker, destroy);
        private static delegate* unmanaged<FT_GlyphRec_**, IntPtr, byte, byte, int> _fnGlyphStrokeBorder;
        public static int GlyphStrokeBorder(FT_GlyphRec_** pglyph, IntPtr stroker, byte inside, byte destroy) => _fnGlyphStrokeBorder(pglyph, stroker, inside, destroy);
        private static delegate* unmanaged<FT_FaceRec_*, FT_Palette_Data_*, int> _fnPaletteDataGet;
        public static int PaletteDataGet(FT_FaceRec_* face, FT_Palette_Data_* apalette) => _fnPaletteDataGet(face, apalette);
        private static delegate* unmanaged<FT_FaceRec_*, ushort, FT_Color_**, int> _fnPaletteSelect;
        public static int PaletteSelect(FT_FaceRec_* face, ushort paletteIndex, FT_Color_** apalette) => _fnPaletteSelect(face, paletteIndex, apalette);

        public static void LoadAll(Func<string, IntPtr> loaderFunc)
        {
            _fnErrorString = (delegate* unmanaged<int, sbyte*>)loaderFunc("FT_Error_String");
            _fnInitFreeType = (delegate* unmanaged<IntPtr, int>)loaderFunc("FT_Init_FreeType");
            _fnDoneFreeType = (delegate* unmanaged<IntPtr, int>)loaderFunc("FT_Done_FreeType");
            _fnNewFace = (delegate* unmanaged<IntPtr, sbyte*, int, FT_FaceRec_**, int>)loaderFunc("FT_New_Face");
            _fnNewMemoryFace = (delegate* unmanaged<IntPtr, byte*, int, int, FT_FaceRec_**, int>)loaderFunc("FT_New_Memory_Face");
            _fnOpenFace = (delegate* unmanaged<IntPtr, FT_Open_Args_*, int, FT_FaceRec_**, int>)loaderFunc("FT_Open_Face");
            _fnAttachFile = (delegate* unmanaged<FT_FaceRec_*, sbyte*, int>)loaderFunc("FT_Attach_File");
            _fnAttachStream = (delegate* unmanaged<FT_FaceRec_*, FT_Open_Args_*, int>)loaderFunc("FT_Attach_Stream");
            _fnReferenceFace = (delegate* unmanaged<FT_FaceRec_*, int>)loaderFunc("FT_Reference_Face");
            _fnDoneFace = (delegate* unmanaged<FT_FaceRec_*, int>)loaderFunc("FT_Done_Face");
            _fnSelectSize = (delegate* unmanaged<FT_FaceRec_*, int, int>)loaderFunc("FT_Select_Size");
            _fnRequestSize = (delegate* unmanaged<FT_FaceRec_*, FT_Size_RequestRec_*, int>)loaderFunc("FT_Request_Size");
            _fnSetCharSize = (delegate* unmanaged<FT_FaceRec_*, int, int, uint, uint, int>)loaderFunc("FT_Set_Char_Size");
            _fnSetPixelSizes = (delegate* unmanaged<FT_FaceRec_*, uint, uint, int>)loaderFunc("FT_Set_Pixel_Sizes");
            _fnLoadGlyph = (delegate* unmanaged<FT_FaceRec_*, uint, int, int>)loaderFunc("FT_Load_Glyph");
            _fnLoadChar = (delegate* unmanaged<FT_FaceRec_*, uint, int, int>)loaderFunc("FT_Load_Char");
            _fnSetTransform = (delegate* unmanaged<FT_FaceRec_*, FT_Matrix_*, FT_Vector_*, void>)loaderFunc("FT_Set_Transform");
            _fnRenderGlyph = (delegate* unmanaged<FT_GlyphSlotRec_*, int, int>)loaderFunc("FT_Render_Glyph");
            _fnGetKerning = (delegate* unmanaged<FT_FaceRec_*, uint, uint, uint, FT_Vector_*, int>)loaderFunc("FT_Get_Kerning");
            _fnGetTrackKerning = (delegate* unmanaged<FT_FaceRec_*, int, int, int*, int>)loaderFunc("FT_Get_Track_Kerning");
            _fnGetGlyphName = (delegate* unmanaged<FT_FaceRec_*, uint, void*, uint, int>)loaderFunc("FT_Get_Glyph_Name");
            _fnGetPostscriptName = (delegate* unmanaged<FT_FaceRec_*, sbyte*>)loaderFunc("FT_Get_Postscript_Name");
            _fnSelectCharmap = (delegate* unmanaged<FT_FaceRec_*, int, int>)loaderFunc("FT_Select_Charmap");
            _fnSetCharmap = (delegate* unmanaged<FT_FaceRec_*, FT_CharMapRec_*, int>)loaderFunc("FT_Set_Charmap");
            _fnGetCharmapIndex = (delegate* unmanaged<FT_CharMapRec_*, int>)loaderFunc("FT_Get_Charmap_Index");
            _fnGetCharIndex = (delegate* unmanaged<FT_FaceRec_*, uint, uint>)loaderFunc("FT_Get_Char_Index");
            _fnGetFirstChar = (delegate* unmanaged<FT_FaceRec_*, uint*, uint>)loaderFunc("FT_Get_First_Char");
            _fnGetNextChar = (delegate* unmanaged<FT_FaceRec_*, uint, uint*, uint>)loaderFunc("FT_Get_Next_Char");
            _fnFaceProperties = (delegate* unmanaged<FT_FaceRec_*, uint, FT_Parameter_*, int>)loaderFunc("FT_Face_Properties");
            _fnGetNameIndex = (delegate* unmanaged<FT_FaceRec_*, sbyte*, uint>)loaderFunc("FT_Get_Name_Index");
            _fnGetSubGlyphInfo = (delegate* unmanaged<FT_GlyphSlotRec_*, uint, int*, uint*, int*, int*, FT_Matrix_*, int>)loaderFunc("FT_Get_SubGlyph_Info");
            _fnGetColorGlyphLayer = (delegate* unmanaged<FT_FaceRec_*, uint, uint*, uint*, FT_LayerIterator_*, byte>)loaderFunc("FT_Get_Color_Glyph_Layer");
            _fnGetFSTypeFlags = (delegate* unmanaged<FT_FaceRec_*, ushort>)loaderFunc("FT_Get_FSType_Flags");
            _fnFaceGetCharVariantIndex = (delegate* unmanaged<FT_FaceRec_*, uint, uint, uint>)loaderFunc("FT_Face_GetCharVariantIndex");
            _fnFaceGetCharVariantIsDefault = (delegate* unmanaged<FT_FaceRec_*, uint, uint, int>)loaderFunc("FT_Face_GetCharVariantIsDefault");
            _fnFaceGetVariantSelectors = (delegate* unmanaged<FT_FaceRec_*, uint*>)loaderFunc("FT_Face_GetVariantSelectors");
            _fnFaceGetVariantsOfChar = (delegate* unmanaged<FT_FaceRec_*, uint, uint*>)loaderFunc("FT_Face_GetVariantsOfChar");
            _fnFaceGetCharsOfVariant = (delegate* unmanaged<FT_FaceRec_*, uint, uint*>)loaderFunc("FT_Face_GetCharsOfVariant");
            _fnMulDiv = (delegate* unmanaged<int, int, int, int>)loaderFunc("FT_MulDiv");
            _fnMulFix = (delegate* unmanaged<int, int, int>)loaderFunc("FT_MulFix");
            _fnDivFix = (delegate* unmanaged<int, int, int>)loaderFunc("FT_DivFix");
            _fnRoundFix = (delegate* unmanaged<int, int>)loaderFunc("FT_RoundFix");
            _fnCeilFix = (delegate* unmanaged<int, int>)loaderFunc("FT_CeilFix");
            _fnFloorFix = (delegate* unmanaged<int, int>)loaderFunc("FT_FloorFix");
            _fnVectorTransform = (delegate* unmanaged<FT_Vector_*, FT_Matrix_*, void>)loaderFunc("FT_Vector_Transform");
            _fnLibraryVersion = (delegate* unmanaged<IntPtr, int*, int*, int*, void>)loaderFunc("FT_Library_Version");
            _fnFaceCheckTrueTypePatents = (delegate* unmanaged<FT_FaceRec_*, byte>)loaderFunc("FT_Face_CheckTrueTypePatents");
            _fnFaceSetUnpatentedHinting = (delegate* unmanaged<FT_FaceRec_*, byte, byte>)loaderFunc("FT_Face_SetUnpatentedHinting");
            _fnNewGlyph = (delegate* unmanaged<IntPtr, int, FT_GlyphRec_**, int>)loaderFunc("FT_New_Glyph");
            _fnGetGlyph = (delegate* unmanaged<FT_GlyphSlotRec_*, FT_GlyphRec_**, int>)loaderFunc("FT_Get_Glyph");
            _fnGlyphCopy = (delegate* unmanaged<FT_GlyphRec_*, FT_GlyphRec_**, int>)loaderFunc("FT_Glyph_Copy");
            _fnGlyphTransform = (delegate* unmanaged<FT_GlyphRec_*, FT_Matrix_*, FT_Vector_*, int>)loaderFunc("FT_Glyph_Transform");
            _fnGlyphGetCBox = (delegate* unmanaged<FT_GlyphRec_*, uint, FT_BBox_*, void>)loaderFunc("FT_Glyph_Get_CBox");
            _fnGlyphToBitmap = (delegate* unmanaged<FT_GlyphRec_**, int, FT_Vector_*, byte, int>)loaderFunc("FT_Glyph_To_Bitmap");
            _fnDoneGlyph = (delegate* unmanaged<FT_GlyphRec_*, void>)loaderFunc("FT_Done_Glyph");
            _fnMatrixMultiply = (delegate* unmanaged<FT_Matrix_*, FT_Matrix_*, void>)loaderFunc("FT_Matrix_Multiply");
            _fnMatrixInvert = (delegate* unmanaged<FT_Matrix_*, int>)loaderFunc("FT_Matrix_Invert");
            _fnOutlineDecompose = (delegate* unmanaged<FT_Outline_*, FT_Outline_Funcs_*, void*, int>)loaderFunc("FT_Outline_Decompose");
            _fnOutlineNew = (delegate* unmanaged<IntPtr, uint, int, FT_Outline_*, int>)loaderFunc("FT_Outline_New");
            _fnOutlineDone = (delegate* unmanaged<IntPtr, FT_Outline_*, int>)loaderFunc("FT_Outline_Done");
            _fnOutlineCheck = (delegate* unmanaged<FT_Outline_*, int>)loaderFunc("FT_Outline_Check");
            _fnOutlineGetCBox = (delegate* unmanaged<FT_Outline_*, FT_BBox_*, void>)loaderFunc("FT_Outline_Get_CBox");
            _fnOutlineTranslate = (delegate* unmanaged<FT_Outline_*, int, int, void>)loaderFunc("FT_Outline_Translate");
            _fnOutlineCopy = (delegate* unmanaged<FT_Outline_*, FT_Outline_*, int>)loaderFunc("FT_Outline_Copy");
            _fnOutlineTransform = (delegate* unmanaged<FT_Outline_*, FT_Matrix_*, void>)loaderFunc("FT_Outline_Transform");
            _fnOutlineEmbolden = (delegate* unmanaged<FT_Outline_*, int, int>)loaderFunc("FT_Outline_Embolden");
            _fnOutlineEmboldenXY = (delegate* unmanaged<FT_Outline_*, int, int, int>)loaderFunc("FT_Outline_EmboldenXY");
            _fnOutlineReverse = (delegate* unmanaged<FT_Outline_*, void>)loaderFunc("FT_Outline_Reverse");
            _fnOutlineGetBitmap = (delegate* unmanaged<IntPtr, FT_Outline_*, FT_Bitmap_*, int>)loaderFunc("FT_Outline_Get_Bitmap");
            _fnOutlineRender = (delegate* unmanaged<IntPtr, FT_Outline_*, FT_Raster_Params_*, int>)loaderFunc("FT_Outline_Render");
            _fnOutlineGetOrientation = (delegate* unmanaged<FT_Outline_*, int>)loaderFunc("FT_Outline_Get_Orientation");
            _fnOutlineGetInsideBorder = (delegate* unmanaged<FT_Outline_*, int>)loaderFunc("FT_Outline_GetInsideBorder");
            _fnOutlineGetOutsideBorder = (delegate* unmanaged<FT_Outline_*, int>)loaderFunc("FT_Outline_GetOutsideBorder");
            _fnStrokerNew = (delegate* unmanaged<IntPtr, IntPtr, int>)loaderFunc("FT_Stroker_New");
            _fnStrokerSet = (delegate* unmanaged<IntPtr, int, int, int, int, void>)loaderFunc("FT_Stroker_Set");
            _fnStrokerRewind = (delegate* unmanaged<IntPtr, void>)loaderFunc("FT_Stroker_Rewind");
            _fnStrokerParseOutline = (delegate* unmanaged<IntPtr, FT_Outline_*, byte, int>)loaderFunc("FT_Stroker_ParseOutline");
            _fnStrokerBeginSubPath = (delegate* unmanaged<IntPtr, FT_Vector_*, byte, int>)loaderFunc("FT_Stroker_BeginSubPath");
            _fnStrokerEndSubPath = (delegate* unmanaged<IntPtr, int>)loaderFunc("FT_Stroker_EndSubPath");
            _fnStrokerLineTo = (delegate* unmanaged<IntPtr, FT_Vector_*, int>)loaderFunc("FT_Stroker_LineTo");
            _fnStrokerConicTo = (delegate* unmanaged<IntPtr, FT_Vector_*, FT_Vector_*, int>)loaderFunc("FT_Stroker_ConicTo");
            _fnStrokerCubicTo = (delegate* unmanaged<IntPtr, FT_Vector_*, FT_Vector_*, FT_Vector_*, int>)loaderFunc("FT_Stroker_CubicTo");
            _fnStrokerGetBorderCounts = (delegate* unmanaged<IntPtr, int, uint*, uint*, int>)loaderFunc("FT_Stroker_GetBorderCounts");
            _fnStrokerExportBorder = (delegate* unmanaged<IntPtr, int, FT_Outline_*, void>)loaderFunc("FT_Stroker_ExportBorder");
            _fnStrokerGetCounts = (delegate* unmanaged<IntPtr, uint*, uint*, int>)loaderFunc("FT_Stroker_GetCounts");
            _fnStrokerExport = (delegate* unmanaged<IntPtr, FT_Outline_*, void>)loaderFunc("FT_Stroker_Export");
            _fnStrokerDone = (delegate* unmanaged<IntPtr, void>)loaderFunc("FT_Stroker_Done");
            _fnGlyphStroke = (delegate* unmanaged<FT_GlyphRec_**, IntPtr, byte, int>)loaderFunc("FT_Glyph_Stroke");
            _fnGlyphStrokeBorder = (delegate* unmanaged<FT_GlyphRec_**, IntPtr, byte, byte, int>)loaderFunc("FT_Glyph_StrokeBorder");
            _fnPaletteDataGet = (delegate* unmanaged<FT_FaceRec_*, FT_Palette_Data_*, int>)loaderFunc("FT_Palette_Data_Get");
            _fnPaletteSelect = (delegate* unmanaged<FT_FaceRec_*, ushort, FT_Color_**, int>)loaderFunc("FT_Palette_Select");
        }
    }
}
