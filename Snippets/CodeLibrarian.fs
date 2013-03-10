GpStructuredStorage file
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   X     �                              
                                                                                 !       "   #   $   %   &   '   (   )                               2           5                                   >           A                                                                           T                                   ]       ^   _   `                                                               q       r   s               x           {       |   }                       �                                                           �           �       �   �               �       �   �   �       �       �               �       �       �   �   �   �   �               �       �               �       �   �   �   �   �   �       �                                   �       �       �       �   �       �                                                                              �                                   �       �                       �                                  Hodge Podge      &   Hodge Podge          VCL   �   �    VCL   �      
 Algorithms     �   
 Algorithms         Application Level Code     �   Application Level Code     '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Database      �   Database          Graphics      �   Graphics          Hardware Stuff   6   �   Hardware Stuff   7       Straight Pascal   D   F   Straight Pascal   E       
 Delphi IDE   c   R   
 Delphi IDE   d       Windows   g   f    Windows   h                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     Topic   Hodge Podge                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         Changing the DBNavigator Glyphs       �   Changing the DBNavigator Glyphs   	   A   # Changing the DBNavigator Glyphs (2)       0  # Changing the DBNavigator Glyphs (2)      E    Data-Aware TDateTimePicker       p   Data-Aware TDateTimePicker      <   ( Graying Out Disabled Data Aware Controls       �
  ( Graying Out Disabled Data Aware Controls      J   " Handling EDBEngineError Exceptions       =  " Handling EDBEngineError Exceptions      D                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              Topic   Database                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           {If you want to change the glyphs on TDBNavigator you must create a new
 component which uses a modified resource file.  The steps are :

 1) copy the file DBCtrls.res to MyDBNavigator.res
 2) change the glyphs in your file 
 3) create a new component TMyDBNavigator as follows:}

Unit MyDBNavigator; 

Interface 

Uses 
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms,Dialogs, 
  ExtCtrls, DBCtrls; 

Type 
  TMyDBNavigator = Class(TDBNavigator) 
    Procedure InitMyButtons; 
  Public 
    Constructor Create(AOwner: TComponent); Override; 
  End; 

Procedure Register; 

Implementation 

{$R *.RES} 

Var 
  BtnTypeName: Array[TNavigateBtn] Of PChar = ('FIRST','PRIOR','NEXT','LAST',
                                               'INSERT','DELETE','EDIT',
                                               'POST','CANCEL','REFRESH'); 

Constructor TMyDBNavigator.Create(AOwner: TComponent); 
Begin 
  Inherited Create(AOwner); 
  InitMyButtons; 
End; 

Procedure TMy   Topic   Changing the DBNavigator Glyphs   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   DBNavigator.InitMyButtons; 
Var 
  Index: TNavigateBtn; 
  ResName: String; 
Begin 
  For Index := Low(Buttons) To High(Buttons) Do Begin 
    FmtStr(ResName, 'dbn_%s', [BtnTypeName[Index]]); 
    Buttons[Index].Glyph.LoadFromResourceName(HInstance, ResName); 
  End; 
End; 

Procedure Register; 
Begin 
  RegisterComponents('My Components', [TMyDBNavigator]); 
End; 

End. 

{Code written by Frank}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
{Just surface the Buttons (protected) array of DbNavigator in a derived
 DbNav component}

type
  TMyDbNav = class (TDbNavigator)
  protected
    function  GetNavButtons (i : TNavigateBtn) : TNavButton;
  public
    property NavButtons [i : TNavigateBtn] : TNavButton read GetNavButtons;
  end;

function  TMyDbNav.GetNavButtons (i : TNavigateBtn) : TNavButton;
begin
  Result := Buttons [i];
end;

{To change the glyph of, say the "Last" button}
  MyDBNav1.NavButtons [nbLast].Glyph.LoadFromFile('myglyph.bmp');

{code written by Binh Ly}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   Topic#   Changing the DBNavigator Glyphs (2)   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               {A Data Aware Time Picker Component}

unit uTFDIDateTime;

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  ComCtrls, dbCtrls, db, ToolWin, Menus,StdCtrls,extctrls;

type
  TFDIDateTime = class(TDateTimePicker)
  private
    FDataLink: TFieldDataLink;
    FFieldName : String;
    function GetDataField: string;
    function GetDataSource: TDataSource;
    function GetField: TField;
    function GetReadOnly: Boolean;
    procedure SetReadOnly(Value: Boolean);
    procedure SetDataField(const Value: string);
    procedure SetDataSource(Value: TDataSource);
    procedure UpdateData(Sender: TObject);
    procedure EditData(Sender: Tobject);
    procedure DataChange(Sender: TObject);
    procedure WMCut(var Message: TMessage); message WM_CUT;
    procedure WMPaste(var Message: TMessage); message WM_PASTE;
  public
    constructor Create( AOwner : TComponent ); override;
    destructor Destroy; override;
    property Field: TField read GetFie   Topic   Data-Aware TDateTimePicker   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ld;
  published
    property DataField: string read GetDataField write SetDataField;
    property DataSource: TDataSource read GetDataSource write SetDataSource;
    property ReadAccess: Boolean read GetReadOnly write SetReadOnly;
    property TabOrder;
  End;

procedure Register;

implementation

constructor TFDIDateTime.Create( AOwner : TComponent );
begin
  inherited;
  FDataLink := TFieldDataLink.Create();
  FDataLink.OnDataChange := DataChange;
  FDataLink.OnUpdateData := UpdateData;
  Self.OnCloseUp := UpdateData;
  Self.OnDropDown := EditData;
  self.OnChange := UpdateData;
End;

destructor TFDIDateTime.Destroy;
Begin
  inherited;
  FDataLink.Free;
  FDataLink := nil;
end;

procedure TFDIDateTime.EditData;
begin
  FDatalink.Edit;
end;

function TFDIDateTime.GetReadOnly: Boolean;
begin
  Result := FDataLink.ReadOnly;
end;

procedure TFDIDateTime.SetReadOnly(Value: Boolean);
begin
  FDataLink.ReadOnly := Value;
end;

function TFDIDateTime.GetDataSource: TDataSource;
begin
  Result := FDataLink.DataSource;
end;

procedure TFDIDateTime.DataChange(Sender: TObject);
begin
  if (FDataLink.Field <> nil) then Begin
    if Self.Kind = dtkDate Then Begin
      If (FDataLink.Field.Value = Null) Then
        self.checked := False
      Else Begin
        self.checked := True;
        self.date := StrToDateTime(FDataLink.Field.Text);
      End;
    End; 
  End;
End;

procedure TFDIDateTime.WMPaste(var Message: TMessage);
begin
  FDataLink.Edit;
end;

procedure TFDIDateTime.WMCut(var Message: TMessage);
begin
  FDataLink.Edit;
end;

procedure TFDIDateTime.SetDataSource(Value: TDataSource);
begin
  FDataLink.DataSource := Value;
  if Value <> nil then 
    Value.FreeNotification(Self);
end;

function TFDIDateTime.GetDataField: string;
begin
  Result := FDataLink.FieldName;
end;

procedure TFDIDateTime.SetDataField(const Value: string);
begin
  FDataLink.FieldName := Value;
end;

function TFDIDateTime.GetField: TField;
begin
  Result := FDataLink.Field;
end;

procedure TFDIDateTime.UpdateData(Sender: TObject);
begin
  FFieldname := self.DataField;
  If (self.Kind = dtkDate) Then
    FDataLink.DataSet.FieldByName(FFieldname).asDateTime := self.Date;
End;

procedure Register;
begin
  RegisterComponents('Stupid Samples', [TFDIDateTime]);
end;

end.

{Code written by Will Wilson}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                {Most data aware components are capable of visually showing that they 
 are disabled (by changing the text color to gray) or enabled (by setting 
 the color to a user-defined windows text color).  Some data aware
 controls such as TDBGrid, TDBRichEdit (in Delphi 3.0) and also TDBEdit 
 (when connected to a numeric or date field) do not display this behavior.

  The code below uses RTTI (Run Time Type Information) to extract
property information and use that information to set the font color to 
gray if the control is disabled. If the control is enabled, the text
color is set to the standard windows text color.
 
  What follows is the step by step creation of a simple example which 
consists of a TForm with a TButton and a TDBRichEdit that
demonstrates this behavior.

  1.  Select File|New Application from the Delphi menu bar.
  2.  Drop a  TDataSource, a TTable, a TButton and a TDBEdit 
      onto the form.
  3.  Set the DatabaseName property of the table to 'DBDEMOS'.
  4.  Set the TableNa   Topic(   Graying Out Disabled Data Aware Controls   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          me property of the table to 'ORDERS.DB'.
  5.  Set the DataSet property of the datasource to 'Table1'.
  6.  Set the DataSource property of the DBEdit to 'DataSource1'.
  7.  Set the DataField property of the DBEdit to 'CustNo'.   
  8.  Set the Active property of the DBEdit to 'False'. 
  9.  Add 'TypInfo' to the uses clause of the form.
 
Below is the actual procedure to put in the implementation
section of your unit:}

// This procedure will either set the text color of a
// dataware control to gray or the user defined color
// constant in clInfoText. 
procedure SetDBControlColor(aControl: TControl);
var
  FontPropInfo: PPropInfo;
begin
  // Check to see if the control is a dataware control
   if (GetPropInfo(aControl.ClassInfo, 'DataSource') <> nil) then begin
     // Extract the front property
     FontPropInfo:= GetPropInfo(aControl.ClassInfo, 'Font');
     // Check if the control is enabled/disabled
     if (aControl.Enabled = false) then
       // If disabled, set the font color to gray
       TFont(GetOrdProp(aControl, FontPropInfo)).Color:= clGrayText
     else
       // If enabled, set the font color to clInfoText
       TFont(GetOrdProp(aControl, FontPropInfo)).Color:= clInfoText;
    end;
end;

{The code for the buttonclick event handler should contain:}

//  This code will cycle through the Controls array and call
//  SetDbControlColor for each control on your form 
//  making sure the font text color is set to what it 
//  should be.

procedure TForm1.Button1Click(Sender: TObject);
var
  i: integer;
begin
  // Loop through the control array
  for i:= 0 to ControlCount-1 do
      SetDBControlColor(Controls[i]);
end;
                                                                                                                                                                                                                                                                                                                                                        {Information that describes the conditions of a database engine error can
be obtained for use by an application through the use of an EDBEngineError
exception. EDBEngineError exceptions are handled in an application through
the use of a try..except construct. When an EDBEngineError exception
occurs, a EDBEngineError object would be created and various fields in that
EDBEngineError object would be used to programmatically determine what
went wrong and thus what needs to be done to correct the situation. Also,
more than one error message may be generated for a given exception. This
requires iterating through the multiple error messages to get needed info-
rmation.

The fields that are most pertinent to this context are:

   ErrorCount: type Integer; indicates the number of errors that are in
     the Errors property; counting begins at zero.

   Errors: type TDBError; a set of record-like structures that contain
     information about each specific error generated; each record is
     accessed   Topic"   Handling EDBEngineError Exceptions   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 via an index number of type Integer.

   Errors.ErrorCode: type DBIResult; indicating the BDE error code for the
     error in the current Errors record.

   Errors.Category: type Byte; category of the error referenced by the
     ErrorCode field.

   Errors.SubCode: type Byte; subcode for the value of ErrorCode.

   Errors.NativeError: type LongInt; remote error code returned from the
     server; if zero, the error is not a server error; SQL statement
     return codes appear in this field.

   Errors.Message: type TMessageStr; if the error is a server error, the
     server message for the error in the current Errors record; if not a
     server error, a BDE error message.

In a try..except construct, the EDBEngineError object is created directly
in the except section of the construct. Once created, fields may be
accessed normally, or the object may be passed to another procedure for
inspection of the errors. Passing the EDBEngineError object to a special-
ized procedure is preferred for an application to make the process more
modular, reducing the amount of repeated code for parsing the object for
error information. Alternately, a custom component could be created to
serve this purpose, providing a set of functionality that is easily trans-
ported across applications. The example below only demonstrates creating
the DBEngineError object, passing it to a procedure, and parsing the
object to extract error information.

In a try..except construct, the DBEngineError can be created with syntax
such as that below}

  procedure TForm1.Button1Click(Sender: TObject);
  var
    i: Integer;
  begin
    if Edit1.Text > ' ' then begin
      Table1.FieldByName('Number').AsInteger := StrToInt(Edit1.Text);
      try
        Table1.Post;
      except on E: EDBEngineError do
        ShowError(E);
      end;
    end;
  end;

{In this procedure, an attempt is made to change the value of a field in a
table and then call the Post method of the corresponding TTable component.
Only the attempt to post the change is being trapped in the try..except
construct. If an EDBEngineError occurs, the except section of the con-
struct is executed, which creates the EDBEngineError object (E) and then
passes it to the procedure ShowError. Note that only an EDBEngineError
exception is being accounted for in this construct. In a real-world sit-
uation, this would likely be accompanied by checking for other types of
exceptions.

The procedure ShowError takes the EDBEngineError, passed as a parameter,
and queries the object for contained errors. In this example, information
about the errors are displayed in a TMemo component. Alternately, the
extracted values may never be displayed, but instead used as the basis for
logic branching so the application can react to the errors. The first step
in doing this is to establish the number of errors that actually occurred.
This is the purpose of the ErrorCount property. This property supplies a
value of type Integer that may be used to build a for loop to iterate
through the errors contained in the object. Once the number of errors
actually contained in the object is known, a loop can be used to visit
each existing error (each represented by an Errors property record) and
extract information about each error to be inserted into the TMemo component}

  procedure TForm1.ShowError(AExc: EDBEngineError);
  var
    i: Integer;
  begin
    Memo1.Lines.Clear;
    Memo1.Lines.Add('Number of errors: ' + IntToStr(AExc.ErrorCount));
    Memo1.Lines.Add('');
    {Iterate through the Errors records}
    for i := 0 to AExc.ErrorCount - 1 do begin
      Memo1.Lines.Add('Message: ' + AExc.Errors[i].Message);
      Memo1.Lines.Add('   Category: ' +
        IntToStr(AExc.Errors[i].Category));
      Memo1.Lines.Add('   Error Code: ' +
        IntToStr(AExc.Errors[i].ErrorCode));
      Memo1.Lines.Add('   SubCode: ' +
        IntToStr(AExc.Errors[i].SubCode));
      Memo1.Lines.Add('   Native Error: ' +
        IntToStr(AExc.Errors[i].NativeError));
      Memo1.Lines.Add('');
    end;
  end;

{Borland TI}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Rotating a Bitmap       f&   Rotating a Bitmap       3    Rotating Fonts    *   .   Rotating Fonts   +   0   ' Loading a JPG from a Paradox Blob Field    ,     ' Loading a JPG from a Paradox Blob Field   -   I    Fast Access to Canvas Pixels    .      Fast Access to Canvas Pixels   /   >     Capturing the Screen to a Bitmap    0   �    Capturing the Screen to a Bitmap   1   B   ( Loading a Bitmap-Pallette From Resources    3   �  ( Loading a Bitmap-Pallette From Resources   4   J                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          Topic   Graphics                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           unit RotateBitmap;

//  Rotating Bitmaps  (from Martin Lord <martinlord@eurobell.co.uk>)
//
// The code snippet "Rotating a Bitmap" gives a general purpose routine
//  to rotate bitmaps.  The routine takes about 60 millisecs for
//  48x48 bitmaps on a Pentium 120MHz
//
//  Trig formulae have been  simplified using ....
//
//        cos(angle+alpha)=cos(angle)cos(alpha)-sin(angle)sin(alpha),
//        sin(angle+alpha)= etc ,
//        Radius*cos(alpha)=x  and  Radius*sin(alpha)=y
//
// When the same bitmap has to be rotated repeatedly by arbitary angles
// - splitting out the code which fills the X,Y-arrays into a separate
//   initialisation procedure would give a further speed gain
//
// Note that rotating a rectangle can "lose" the corners and this
// procedure uses the topleft pixel to "fill in" the corners.

interface

uses
  Windows, Graphics, Math, SysUtils;

function BmpRotate(var Src, Dst: tbitmap; angle: Extended): Boolean;

implementation

function BmpRotate(var Src, Ds   Topic   Rotating a Bitmap   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 t: tbitmap; angle: Extended): Boolean;
var
  c1x, c1y, c2x, c2y: Integer;
  p1x, p1y, p2x, p2y: Integer;
  j, k, n: Integer;
  sinAngle, cosAngle: Single;
  P, P2: PByteArray;
  X: array of array of Byte;
  Y: array of array of array of Byte; // pf 15,24
  b, bit, mask: Byte;
  top_left1, top_left2, top_left3: Byte;
  ////   The following builds up source pixel arrays for each format
  ////   if you are rotating the same bitmap thru arbitary angles
  ////   making this block a separate procedure called once only
  ////   will give a further speed improvement
  //******************* getpf1bitX *************************
  procedure getpf1bitX;
  var
    nhBytes: Integer;
    p2x, p2y: Integer;
  begin
    SetLength(X, (Src.Width div 8), Src.Height);
    nhBytes := (Src.Width div 8); // Number of bytes to contain Src.width bits
    if (Src.Width mod 8) > 0 then
      Inc(nhBytes);
    for p2y := 0 to Src.Height - 1 do
    begin
      P := Src.Scanline[p2y];
      p2x := 0;
      while p2x < nhBytes do
      begin
        b := p[p2x];
        x[p2x, p2y] := b;
        Inc(p2x);
      end;
    end;
    top_left1 := (X[0, 0] shr 7) shl 7; // Use topleft pixel for points rotated "into" Dst
  end;

  //******************* getpf4bitX *************************
  procedure getpf4bitX;
  var
    p2y: Integer;
  begin
    SetLength(X, Src.Width + 1, Src.Height);
    for p2y := 0 to Src.Height - 1 do
    begin
      P := Src.Scanline[p2y];
      k := 0;
      j := 0;
      while j < Src.Width do
      begin
        b := p[k]; // Unpack pixels (4bits per pixel)
        x[j, p2y] := (b shr 4);
        Inc(j);
        x[j, p2y] := (b and $0F);
        Inc(j);
        Inc(k);
      end;
    end;
    top_left1 := X[0, 0]; // Use topleft pixel for points rotated "into" Dst
  end;

  //******************* getpf8bitX *************************
  procedure getpf8bitX;
  var
    p2y: Integer;
  begin
    SetLength(X, Src.Width, Src.Height);
    for p2y := 0 to Src.Height - 1 do
    begin
      P := Src.Scanline[p2y];
      j := 0;
      while j < Src.Width do
      begin // 1 byte per pixel
        b := p[j];
        x[j, p2y] := b;
        Inc(j);
      end;
    end;
    top_left1 := X[0, 0]; // Use topleft pixel for points rotated "into" Dst

  end;

  //******************* getpf15bitX *************************
  procedure getpf15bitX;
  var
    p2y: Integer;
  begin
    SetLength(Y, 2, Src.Width, Src.Height);
    for p2y := 0 to Src.Height - 1 do
    begin
      P := Src.Scanline[p2y];
      k := 0;
      j := 0;
      while j < Src.Width do
      begin
        b := p[k];
        Y[0, j, p2y] := b;
        b := p[k + 1];
        Y[1, j, p2y] := b;
        Inc(j);
        Inc(k, 2);
      end;
    end;
    top_left1 := Y[0, 0, 0]; // Use topleft pixel for points rotated "into" Dst
    top_left2 := Y[1, 0, 0];
  end;

  //*********** getpf24bitX *************************
  procedure getpf24bitX;
  var
    p2y: Integer;
  begin
    SetLength(Y, 3, Src.Width, Src.Height);
    for p2y := 0 to Src.Height - 1 do
    begin
      P := Src.Scanline[p2y];
      k := 0;
      j := 0;
      while j < Src.Width do
      begin
        b := p[k];
        Y[0, j, p2y] := b;
        b := p[k + 1];
        Y[1, j, p2y] := b;
        b := p[k + 2];
        Y[2, j, p2y] := b;
        Inc(j);
        Inc(k, 3);
      end;
    end;
    top_left1 := Y[0, 0, 0]; // Use topleft pixel for points rotated "into" Dst
    top_left2 := Y[1, 0, 0];
    top_left3 := Y[2, 0, 0];
  end;

begin
  Result := False; // Check pixelformats....
  if (Src.pixelformat <> Dst.pixelformat) then Exit;
  Result := True;
  Dst.Canvas.Pen.Color := clBlack;
  Dst.Canvas.Brush.Color := clBlack;
  Dst.Canvas.FillRect(Dst.Canvas.ClipRect);

  // Angle in radians
  sinAngle := sin(angle);
  cosAngle := cos(angle);

  // Calculate the central points
  c1x := Src.Width div 2;
  c1y := Src.Height div 2;
  c2x := Dst.Width div 2;
  c2y := Dst.Height div 2;

  // Build up pixel arrays (X or Y)
  case Src.pixelformat of
    pf1bit: getpf1bitX;
    pf4bit: getpf4bitX;
    pf8bit: getpf8bitX;
    pf15bit: getpf15bitX;
    pf24bit: getpf24bitX;
  end;

  // Do the rotation
  case Dst.pixelformat of
    //////////////////////////pf1bit////////////////////////
    pf1bit:
      for p2y := 0 to Src.Height - 1 do
      begin
        P2 := Dst.Scanline[p2y];
        for p2x := 0 to Src.Width - 1 do
        begin
          p1x := c1x + round((p2x - c2x) * cosAngle - (p2y - c2y) * sinAngle);
          p1y := c1y + round((p2x - c2x) * sinAngle + (p2y - c2y) * cosAngle);
          if (p1x < 0) or (p1x >= Dst.Width)
            or (p1y >= Dst.Height) or (p1y < 0) then // Point rotated from outside of bitmap - set source bit to top left
            bit := top_left1
          else
          begin // Find the byte for the p1x point
            n := (p1x div 8);
            b := x[n, p1y]; // Contains the required bit
            mask := (1 shl (7 - (p1x mod 8))); // Mask for the source bit
            bit := mask and b; // Get the source bit
            bit := bit shl (((c1x + p1x) mod 8)); // Shift source bit to leftmost
          end;
          n := (p2x div 8);
          b := p2[n]; // Contains  the destination bit
          mask := 1 shl (7 - (p2x mod 8)); // Mask for the destination bit
          b := b and (not mask); // Clear the destination bit
          bit := bit shr ((p2x mod 8)); // Shift the source bit to the destination position
          p2[n] := b or bit; // Set it in the byte
        end;
      end;

    ////////////////////pf4bit/////////////////////////
    pf4bit:
      for p2y := 0 to Dst.Height - 1 do
      begin
        P2 := Dst.Scanline[p2y];
        k := 0;
        p2x := 0;
        while p2x < Dst.Width do
        begin
          // Get the source coords (p1x,p1y)...
          // ....corresponding to destination (p2x,p2y)
          p1y := c1y + round((p2x - c2x) * sinAngle + (p2y - c2y) * cosAngle);
          p1x := c1x + round((p2x - c2x) * cosAngle - (p2y - c2y) * sinAngle);
          if (p1x < 0) or (p1y < 0)
            or (p1x >= Src.Width) or (p1y >= Src.Height) then
            b := top_left1 // Src out of range use top_left
          else
            b := x[p1x, p1y]; // Source pixel
          if (p2x and 1) = 0 then
            p2[k] := Byte(b shl 4) // Pack pixel 4bits into the Dst byte
          else
          begin
            p2[k] := p2[k] or b;
            Inc(k);
          end;
          Inc(p2x);
        end;
      end;

    ///////////////////////pf8bit//////////////
    pf8bit:
      for p2y := 0 to Dst.Height - 1 do
      begin
        P2 := Dst.Scanline[p2y];
        p2x := 0;
        while p2x < Dst.Width do
        begin
          // Get the source coords (p1x,p1y)...
          // ....corresponding to destination (p2x,p2y)
          p1y := c1y + round((p2x - c2x) * sinAngle + (p2y - c2y) * cosAngle);
          p1x := c1x + round((p2x - c2x) * cosAngle - (p2y - c2y) * sinAngle);
          if (p1x < 0) or (p1y < 0)
            or (p1x >= Src.Width) or (p1y >= Src.Height) then
            b := top_left1 // Out of range
          else
            b := x[p1x, p1y]; // Source pixell
          p2[p2x] := b;
          Inc(p2x);
        end;
      end; // pf8bit

    //////////////////////////////////pf15bit,pf24bit
    pf15bit, pf24bit:
      for p2y := 0 to Dst.Height - 1 do
      begin
        P2 := Dst.Scanline[p2y];
        k := 0;
        p2x := 0;
        while p2x < Dst.Width do
        begin
          // Get the source coords (p1x,p1y)...
          // ....corresponding to destination (p2x,p2y)
          p1y := c1y + round((p2x - c2x) * sinAngle + (p2y - c2y) * cosAngle);
          p1x := c1x + round((p2x - c2x) * cosAngle - (p2y - c2y) * sinAngle);
          if (p1x < 0) or (p1y < 0) or (p1x >= Src.Width) or (p1y >= Src.Height) then
          begin // Out of range
            p2[k] := top_left1;
            p2[k + 1] := top_left2;
            Inc(k, 2);
            if Src.pixelformat = pf24bit then
            begin
              p2[k] := top_left3;
              Inc(k);
            end;
          end
          else
          begin
            p2[k] := y[0, p1x, p1y]; // Source pixell
            p2[k + 1] := y[1, p1x, p1y];
            Inc(k, 2);
            if Src.pixelformat = pf24bit then
            begin
              P2[k] := y[2, p1x, p1y];
              Inc(k);
            end
          end;
          Inc(p2x);
        end;
      end;
  end; // case

  Finalize(x);
  Finalize(y);
end;

end.

                                                                                                                                                                                                                                                                                                                                                                                                                          {Rotating fonts is a straight forward process, so long as the Windows font 
 mapper can supply a rotated font based on the font you request. Using a\
 TrueType font virtually guarantees success.
  
 Here is an example of creating a font that is rotated 45 degrees:}

procedure TForm1.Button1Click(Sender: TObject);
var
  lf : TLogFont;
  tf : TFont;
begin
  with Form1.Canvas do begin
    Font.Name := 'Arial';
    Font.Size := 24;
    tf := TFont.Create;
    try
      tf.Assign(Font);
      GetObject(tf.Handle, sizeof(lf), @lf);
      lf.lfEscapement := 450;
      lf.lfOrientation := 450;
      tf.Handle := CreateFontIndirect(lf);
      Font.Assign(tf);
    finally
      tf.Free;
    end;
    TextOut(20, Height div 2, 'Rotated Text!');
  end;
end;

{found on the Borland Forums}                                                                                                                                                                                                                     Topic   Rotating Fonts   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    {Here's an example that displays a Jpeg image that is stored in
a paradox blob field:}

  procedure LoadJPGFromBlob;
  var
    aJpeg  : TJPEGImage;
    aStream: TMemoryStream;
  begin
    try
      aJpeg   := TJPEGImage.Create;
      aStream := TMemoryStream.Create;
      tabJDataTheData.SaveToStream(aStream);
      aStream.Seek(0,soFromBeginning);
      aJpeg.LoadFromStream(aStream);
      Image2.Picture.Assign(aJpeg);
    finally
      aJpeg.Free;
      aStream.Free;
    end;
  end;

{coded by Bill}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  Topic'   Loading a JPG from a Paradox Blob Field   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           {here is my solution for !Fast! accessing pixels on a bitmap by setting
RGB-Values for each pixel:}

{1) use TBitmap
2) load a 24Bit-Bitmap into it (loadfromfile / loadfromstream)
3) now you can use the scanline-property of D3 to access a whole pixel-line
4) the RGB-Values can be set by a loop:}

    P := Bitmap.Scanline[y];
    x := 0;
    while x <= Bitmap.width*3 -1 do
    begin
      P[x] := 200; //Blue
      P[x+1] := 200; //Green
      P[x+2] := 200; //Red
      inc(x,3)
    end;

{coded by Boris Nienke}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Topic   Fast Access to Canvas Pixels   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      procedure ScreenShot(x, y, Width, Height: Integer; bm: TBitMap);
var
  dc: HDC;
  lpPal: PLOGPALETTE;
begin
  // Test width and height
  if ((Width <= 0) or (Height <= 0)) then
    Exit;
  bm.Width := Width;
  bm.Height := Height;
  // Get the screen dc
  dc := GetDc(0);
  if (dc = 0) then
    Exit;
  // Do we have a palette device?
  if (GetDeviceCaps(dc, RASTERCAPS) and RC_PALETTE = RC_PALETTE) then
  begin
    // Allocate memory for a logical palette
    GetMem(lpPal, sizeof(TLOGPALETTE) + (255 * sizeof(TPALETTEENTRY)));
    // Zero it out to be neat
    FillChar(lpPal^, sizeof(TLOGPALETTE) + (255 * sizeof(TPALETTEENTRY)), #0);
    // Fill in the palette version
    lpPal^.palVersion := $300;
    // Grab the system palette entries
    lpPal^.palNumEntries :=
        GetSystemPaletteEntries(dc, 0, 256, lpPal^.palPalEntry);
    if (lpPal^.PalNumEntries <> 0) then
      bm.Palette := CreatePalette(lpPal^);
    FreeMem(lpPal, sizeof(TLOGPALETTE) +
      (255 * sizeof(TPALETTEENTRY)   Topic    Capturing the Screen to a Bitmap   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  ));
  end;
  // Copy from the screen to the bitmap
  BitBlt(bm.Canvas.Handle, 0, 0, Width, Height, Dc, x, y, SRCCOPY);
  // Release the screen dc
  ReleaseDc(0, dc);
end;

{ Originally from Joe C. Hecht }

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        {If 256 color bitmaps are in the resource file, assigning the handle
doesn't assign the palette. Thus, the 256 color bitmaps will end up
dithered. I use the following function to load bitmaps from resource
files}

function LoadBitmapFromResource(inst: THandle;
                                resnum: Word;
                                outBmp: TBitmap): Boolean;
var
  HResInfo: THandle;
  BMF: TBitmapFileHeader;
  MemHandle: THandle;
  Stream: TMemoryStream;
  ResPtr: PByte;
  ResSize: Longint;
begin
  result := false;
  BMF.bfType := $4D42;
  HResInfo := FindResource(inst,MakeIntResource(resnum),RT_Bitmap);
  if hResInfo = 0 then
    exit;
  ResSize := SizeofResource(inst, HResInfo);
  MemHandle := LoadResource(inst, HResinfo);
  if MemHandle = 0 then
    exit;
  try
    ResPtr := LockResource(MemHandle);
    Stream := TMemoryStream.Create;
    try
      Stream.SetSize(ResSize + SizeOf(BMF));
      Stream.Write(BMF, SizeOf(BMF));
      Stream.Write(ResPtr^, ResSize);
      St   Topic(   Loading a Bitmap-Pallette From Resources   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          ream.Seek(0, 0);
      outBmp.LoadFromStream(Stream);
      result := true;
    finally
      Stream.Free;
    end;
  finally
    FreeResource(MemHandle);
  end;
end;

{coded by Marc Batchelor}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   Finding out the CPU Speed    8   �   Finding out the CPU Speed   9   ;   " Silently Checking the Floppy Drive    :   G  " Silently Checking the Floppy Drive   ;   D   " Reading the Hard Drive Volume Name    <   �  " Reading the Hard Drive Volume Name   =   D   ! Dialing a Phone Number using TAPI    ?   �  ! Dialing a Phone Number using TAPI   @   C    Detecting Drive Types    B   A   Detecting Drive Types   C   7                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Topic   Hardware Stuff                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     program CpuSpeed; 

uses SysUtils, Windows, Dialogs;

function GetCpuSpeed: Comp;
var
  t: DWORD;
  mhi, mlo, nhi, nlo: DWORD;
  t0, t1, chi, clo, shr32: Comp;
begin
  shr32 := 65536; 
  shr32 := shr32 * 65536;

  t := GetTickCount; 
  while t = GetTickCount do begin end;
  asm
    DB 0FH
    DB 031H
    mov mhi,edx
    mov mlo,eax
  end;

  while GetTickCount < (t + 1000) do begin end;
  asm
    DB 0FH
    DB 031H
    mov nhi,edx
    mov nlo,eax
  end;

  chi := mhi; if mhi < 0 then chi := chi + shr32;
  clo := mlo; if mlo < 0 then clo := clo + shr32;

  t0 := chi * shr32 + clo;

  chi := nhi; if nhi < 0 then chi := chi + shr32;
  clo := nlo; if nlo < 0 then clo := clo + shr32;

  t1 := chi * shr32 + clo;

  Result := (t1 - t0) / 1E6;
end;

begin
  MessageDlg(Format('%.1f MHz', [GetCpuSpeed]), mtConfirmation, [mbOk], 0);
end.

{coded by Tony Olekshy}
                                                                                                                   Topic   Finding out the CPU Speed   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         {With this method you can check a drive and fail with out
 prompting the user with the BUMB (Big Ugly Message Box)}

var
  ErrorMode : Word;
begin
  ErrorMode := SetErrorMode(SEM_FAILCRITICALERRORS);
  try
    { Check for drive here }
  finally
    SetErrorMode(ErrorMode);
  end;
end;

{coded by Rick Rogers}

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Topic"   Silently Checking the Floppy Drive   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                

type
  MIDPtr = ^MIDRec;
  MIDRec = Record
    InfoLevel: word;
    SerialNum: LongInt;
    VolLabel: Packed Array [0..10] of Char;
    FileSysType: Packed Array [0..7] of Char;
  end;

var
  Info: MIDRec;

function GetDriveSerialNum(MID: MIDPtr; drive: Word): Boolean; assembler;
asm
  push  DS    { Just for safety, I dont think its really needed }
  mov   ax,440Dh { Function Get Media ID }
  mov   bx,drive    { drive no (0-Default, 1-A ...) }
  mov   cx,0866h  { category and minor code }
  lds   dx,MID      { Load pointeraddr. }
  call  DOS3Call   { Supposed to be faster than INT 21H }
  jc    @@err
  mov   al,1           { No carry so return TRUE }
  jmp   @@ok
 @@err:
  mov   al,0           { Carry set so return FALSE }
 @@ok:
  pop   DS            { Restore DS, were not supposed to change it }
end;

{To read the Serial Number}
function ReadSerial:string;
begin
  if GetDriveSerialNum(@info,0) then 
    Result := IntToStr(info.SerialNum);
end;

{To read the Vol Label}   Topic"   Reading the Hard Drive Volume Name   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
function ReadVol:string;
begin
  if GetDriveSerialNum(@info,0) then 
    Result := StrPas(Info.VolLabel);
end;

{found by Brian}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       {declares for simple tapi}
  TAPIMAXDESTADDRESSSIZE  = 80;
  TAPIMAXAPPNAMESIZE      = 40;
  TAPIMAXCALLEDPARTYSIZE  = 40;
  TAPIMAXCOMMENTSIZE      = 80;

function tapiRequestMakeCall      ; external 'Tapi32.dll';

Function  DialPhone (PhoneNbr, CalledParty, Comment : String) : Boolean;
Var 
  MyPhoneNbr : Pchar;
  MyAppName : Pchar;
  MyCalledParty : Pchar;
  MyComment : Pchar;
Begin
  Result := false;
  If (length(PhoneNbr) > TAPIMAXDESTADDRESSSIZE) or
     (length(CalledParty) > TAPIMAXCALLEDPARTYSIZE) or
     (length(Comment) > TAPIMAXCOMMENTSIZE) then 
    exit;

  myPhoneNbr := StrAlloc(TAPIMAXDESTADDRESSSIZE);
  MyAppName := StrAlloc(TAPIMAXAPPNAMESIZE);
  MyCalledParty := StrAlloc(TAPIMAXCALLEDPARTYSIZE);
  MyComment := StrAlloc(TAPIMAXCOMMENTSIZE);
  try
    StrPCopy(MyPhoneNbr, PhoneNbr);
    StrPCopy(MyCalledParty, CalledParty);
    StrPCopy(MyComment, Comment);
    StrPCopy(MyAppName, 'Whatever');
    Result := tapiRequestMakeCall(MyPhoneNbr, MyAppName, 
             Topic!   Dialing a Phone Number using TAPI   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         MyCalledParty,MyComment) = 0;
  finally
    StrDispose(MyPhoneNbr);
    StrDispose(MyAppName);
    StrDispose(MyCalledParty);
    StrDispose(MyComment);
  end;
end;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           procedure TForm1.Button1Click(Sender: TObject);
var
  Drive: Char;
  DriveLetter: String[4];
begin
  for Drive := 'A' to 'Z' do
  begin
    DriveLetter := Drive + ':\';
    case GetDriveType(PChar(Drive + ':\')) of
      DRIVE_REMOVABLE:
        Memo1.Lines.Add(DriveLetter + '     Floppy Drive');
      DRIVE_FIXED:
        Memo1.Lines.Add(DriveLetter + '     Fixed Drive');
      DRIVE_REMOTE:
        Memo1.Lines.Add(DriveLetter + '     Network Drive');
      DRIVE_CDROM:
        Memo1.Lines.Add(DriveLetter + '     CD-ROM Drive');
    end;
  end;
end;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                  Topic   Detecting Drive Types   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              Using TypInfo and RTTI    F   W   Using TypInfo and RTTI   G   8    Starting an External Program    H   =    Starting an External Program   I   >    Dynamic Arrays    J   �   Dynamic Arrays   K   0    Direct Memory Access    L   �   Direct Memory Access   M   6   ! Checking the State of Toggle Keys    N   D  ! Checking the State of Toggle Keys   O   C    Clipper Style IIF    P   ?   Clipper Style IIF   Q   3    Path and Filename Tricks    R   �   Path and Filename Tricks   S   :    String Manipulations   U   >   String Manipulations   V   %                                                                                                                                                                                                                                                                                                                                                                                                                                                                  Topic   Straight Pascal                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    {Here is an example of how to access a components published property
 using the published properties name}
 
function  GetSuggestion(AComponent: TComponent;
                        var Caption : string): boolean;
var
  PropInfo : PPropInfo;
begin
  Result := false ;
  Caption := '' ;
  PropInfo := GetPropInfo(AComponent.ClassInfo, 'Caption');
  If ( PropInfo = NIL ) or 
     not ( PropInfo.PropType^.Kind in 
           [ tkString, tkLString, tkWString ] ) then
    exit ;
  Caption := GetStrProp( AComponent, PropInfo ) ;
  Result := true ;
end;

{coded by Mike Scott (TeamB)}                                                                                                                                                                                                                                                                                                                                                                                                                                            Topic   Using TypInfo and RTTI   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            ExecuteFile('C:\WINDOWS\EXPLORER.EXE', '/e, D:\','',SW_SHOW);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      Topic   Starting an External Program   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      {To get a dynamically sized array in pascal, declare a type of that array
at its maximum size.  Then, your array will be a pointer to this type
and you can reallocate how much memory it uses at any time.

For example: }

Type
  PDynamicArray = ^TDynamicArray;
  TDynamicArray = array[0..MaxListSize - 1] of Integer;  
var
  DynamicArray : PDynamicArray;
begin
  GetMem(DynamicArray, (DesiredSize) * SizeOf(Integer));
  //When accessing items in the array, you must first dereference the pointer
  DynamicArray^[0] := 3;
end;
  
{To resize the array you allocate a new array at the new size, then 'move'
 items from the old to the new.  (The easiest way to do this is to use the
 move command}
                                                                                                                                                                                                                                                                                                                          Topic   Dynamic Arrays   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    {How can I (using Delphi 1.0) access memory directly? I have to}
{access an expansion card which read memory $C800 - $C9FF. I}
{understood that accessing this memory under protected mode is
{done via selectors.}

  Device_Selector:=AllocSelector(DSeg);            
  SetSelectorBase(Device_Selector,BaseMem shl 4);   
  SetSelectorLimit(Device_Selector,SizeInBytes); 
  DevicePoint:=Ptr(Device_Selector,0);        

{Code written by Victor}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 Topic   Direct Memory Access   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              {A very simple function that returns the toggle status of key. You pass the
 key (VK_NUMLOCK, VK_INSERT, etc.) and the it returns whether the key is
 toggled on or off.}

function GetToggleState(Key: integer): boolean;
begin
  Result := Odd(GetKeyState(Key));
end;
                        
{donated by Erik Berry}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               Topic!   Checking the State of Toggle Keys   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 function IIF(Condition: Boolean; X, Y: Variant): Variant;

// Note that both variants are fullty evaluated for every call
// to IIF, so it is best to make them quick calculations
function IIF(Condition: Boolean; X, Y: Variant): Variant;
begin
  if Condition then
    Result := X
  else
    Result := Y;
end;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Topic   Clipper Style IIF   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 // From Carl Dippel <carl@imcnyc.com>
uses ShellApi;

// Returns the number of backslashes in a path
function BackSlashes(Path: string): Integer;
var
  i: Integer;
begin
  Result := 0;
  for i := 1 to Length(Path) do
    Result := Result + Ord(path[i] = '\');
end;

// Returns true if the path is a network resource i.e. \\machine\resource
function NetResource(path:string):Boolean;
begin
  Result:=(Copy(path,1,2)='\\') and (BackSlashes(path)=3)
end;

function Expand(Path: string): string;
var
  Info: TSHFileInfo;
begin
  Result := ExtractFileDir(Path);    // remove last element if there is one
  if Result = path then              // reached root: recurision is complete
    Delete(Result, Length(Result), 1)// remove that irksome backslash
  else if NetResource(Path) then     // network resources cannot be expanded
    Result := Path                   // recursion is complete
  else if SHGetFileInfo(PChar(Path), 0, Info, SizeOf(Info), SHGFI_DISPLAYNAME) = 1 then
    Result := Expand(R   Topic   Path and Filename Tricks   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          esult) + '\' + Info.szDisplayName // recursively expand the rest
  else
    raise Exception.CreateFmt('%s is not a valid path', [Path])  { woops }
end;

function LongPath(Path: string): string;
{ changes an 8.3 \path\filename like "c:\progra~1\test.doc" }
{ to a long \path\filename like "c:\program files\test.doc" }
begin
  // expand absolute path from possible relative one
  Result := Expand(ExpandFilename(uppercase(path)));
end;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    String to Enumerated Conversion    W   U   String to Enumerated Conversion   X   A    Fixing capitalization    Y      Fixing capitalization   Z   7   " A Delphi Version of the C sscanf()    [   �  " A Delphi Version of the C sscanf()   \   D    Fast String Scan    a   h   Fast String Scan   b   2                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          Topic   String Manipulations                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               {for enumerated types you can indeed convert between string name and ordinal
 value at runtime using functions from the TypInfo unit.}

theStringname := GetEnumName(Typeinfo(TAlignment),Ord(label1.Alignment));
  
label1.Alignment := TAlignment(GetEnumValue(TypeInfo(TAlignment),theStringname));

{code written by Peter Below (TeamB) }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              Topic   String to Enumerated Conversion   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   {You can switch the delimeters to be punctuation marks if that is more
 appropriate to your needs.}

procedure Recapitalize(var s: String);
var
  delimiter : Boolean;
  i : LongInt;
begin
  delimiter := True;
  for i := 1 to Length(s) do
    if s[i] in [' ', #9, '\'] then
       delimiter := True
    else if delimiter then begin
       if s[i] in ['a'..'z'] then
         s[i] := Chr(Ord(s[i]) - 32);
       delimiter := False;
    end else if s[i] in ['A'..'Z'] then
      s[i] := Chr(Ord(s[i]) + 32);
end;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  Topic   Fixing capitalization   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             {This code will be commented / cleaned up in the next release

 Sscanf parses an input string. The parameters ...
    s - input string to parse
    fmt - 'C' scanf-like format string to control parsing
      %d - convert a Long Integer
      %f - convert an Extended Float
      %s - convert a string (delimited by spaces)
      other char - increment s pointer past "other char"
      space - does nothing
    Pointers - array of pointers to have values assigned

    result - number of variables actually assigned

    for example with ...
      Sscanf('Name. Bill   Time. 7:32.77   Age. 8',
             '. %s . %d:%f . %d', [@Name, @hrs, @min, @age]);
    an extended has to have a delim beyond the last digit: 32.77;
        otherwise the last digit is stripped

    You get ...
      Name = Bill  hrs = 7  min = 32.77  age = 8                
          
procedure usescan;
var Name: shortstring; hrs, age: longint; min: extended;
  nargs, j: integer;
begin
  nargs := Sscanf('Name. Bill  x T   Topic"   A Delphi Version of the C sscanf()   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ime. 7:32.775 8.',
    '. %s . %d:%f %d', [@Name, @hrs, @min, @age]);
  if nargs >= 1 then ShowMessage(Format('The name is %s ', [Name]));
  if nargs >= 2 then ShowMessage(Format('the hours are %d ', [hrs]));
  if nargs >= 3 then ShowMessage(Format('the minutes are %.5f ', [min]));
  if nargs >= 4 then ShowMessage(Format(' The age is %d ', [age]));
end;

          }

unit Scanf;

interface
uses 
  SysUtils;

type
  EFormatError = class(ExCeption);
  
function Sscanf(const s: string; const fmt : string;
                const Pointers : array of Pointer) : Integer;

implementation

function Sscanf(const s: string; const fmt : string;
                const Pointers : array of Pointer) : Integer;
var
  i,j,n,m : integer;
  s1 : shortstring;
  L : LongInt;
  X : Extended;

  function GetInt : Integer;
  begin
    s1 := '';
    while (n <= length(s)) and (s[n] = ' ') do 
      inc(n);
    while (n <= length(s)) and (s[n] in ['0'..'9', '+', '-']) do begin
      s1 := s1+s[n];
      inc(n);
    end;
    Result := Length(s1);
  end;

  function GetFloat : Integer;
  begin
    s1 := '';
    while (n <= length(s)) and (s[n] = ' ') do 
      inc(n);
    while (n <= length(s)) and //jd >= rather than >
          (s[n] in ['0'..'9', '+', '-', '.', 'e', 'E']) do begin
      s1 := s1+s[n];
      inc(n);
    end;
    Result := Length(s1);
  end;

  function GetString : Integer;
  begin
    s1 := '';
    while (n <= length(s)) and (s[n] = ' ') do 
      inc(n);
    while (n <= length(s)) and (s[n] <> ' ') do begin
      s1 := s1+s[n];
      inc(n);
    end;
    Result := Length(s1);
  end;

  function ScanStr(c : Char) : Boolean;
  begin
    while (n <= length(s)) and (s[n] <> c) do 
      inc(n);
    inc(n);

    result := (n <= length(s));
  end;

  function GetFmt : Integer;
  begin
    Result := -1;

    while (TRUE) do begin
      while (fmt[m] = ' ') and (Length(fmt) > m) do 
        inc(m);
      if (m >= Length(fmt)) then 
        break;
      if (fmt[m] = '%') then begin
        inc(m);
        case fmt[m] of
          'd': Result := vtInteger;
          'f': Result := vtExtended;
          's': Result := vtString;
        end;
        inc(m);
        break;
      end;
      if (ScanStr(fmt[m]) = False) then 
        break;
      inc(m);
    end;
  end;

begin
  n := 1;
  m := 1;
  Result := 0;

  for i := 0 to High(Pointers) do begin
    j := GetFmt;

    case j of
      vtInteger : begin
        if GetInt > 0 then begin
          L := StrToInt(s1);
          Move(L, Pointers[i]^, SizeOf(LongInt));
          inc(Result);
        end else 
          break;
      end;

      vtExtended : begin
        if GetFloat > 0 then begin
          X := StrToFloat(s1);
          Move(X, Pointers[i]^, SizeOf(Extended));
          inc(Result);
        end else 
          break;
      end;

      vtString : begin
        if GetString > 0 then begin
          Move(s1, Pointers[i]^, Length(s1)+1);
          inc(Result);
        end else 
          break;
      end;
      
      else  {case}
        break;
    end; {case}
  end;
end;

end.
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  {Although StrScan is in Assembler, this is faster, about 40%}

function ScanStr(ToScan: PChar; Sign: Char):PChar;
begin
  Result:= nil;
  if ToScan <> nil then
    while (ToScan^ <> #0) do begin
      if ToScan^ = Sign then begin
        Result:= ToScan;
        break;
       end;
     inc(ToScan);
    end;
end;

{donated by Martin Waldenburg}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           Topic   Fast String Scan   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   Viewing the IDE CPU Window    e   |   Viewing the IDE CPU Window   f   <                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      Topic
   Delphi IDE                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         {Curious as to how Gerald enabled the CPU window in Delphi?  He
 simply addded the following registry string key with a value of '1'
 
HKEY_CURRENT_USER\Software\Borland\Delphi\3.0\Debugging

The CPU window is useful for seeing the assembly equivalents of code,
but was not completely coded by Borland and by default pops up on some
errors when you really don't want it.}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       Topic   Viewing the IDE CPU Window   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         Windows API   i   h   Windows API   j       Shell Stuff   �   &   Shell Stuff   �                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     Topic   Windows                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             ScreenSaver Active Check    k   I    ScreenSaver Active Check   l   :   ( Reading the Binary Type of an Executable    m     ( Reading the Binary Type of an Executable   n   J    Intellimouse Specifications    o      Intellimouse Specifications   p   =    Finding Out the Default Browser    t   A   Finding Out the Default Browser   u   A   + Converting Between Long and Short Filenames    v     + Converting Between Long and Short Filenames   w   M    BrowseForFolder Wrapper    y   �   BrowseForFolder Wrapper   z   9    Is a Mouse Present?    ~   �    Is a Mouse Present?      5    Fake the PrintScrn Key    �   �   Fake the PrintScrn Key   �   8    Administrator Rights under NT    �   �   Administrator Rights under NT   �   ?   % Using SetForegroundWindow under Win98    �   �  % Using SetForegroundWindow under Win98   �   G                                                                                                                                                                Topic   Windows API                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        SystemsParametersInfo( SPI_GETSCREENSAVEACTIVE, 0, @aBoolVariable, 0 );
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          Topic   ScreenSaver Active Check   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          {The function call GetBinaryType is in Windows.pas
function GetBinaryType(lpApplicationName: PChar; var lpBinaryType: DWORD): BOOL; stdcall;

Below is an example of how to use it}

Var 
  Filename, S: String;
  BinaryType: DWORD;
begin   
  Filename := 'whatever';
  If GetBinaryType(Pchar(Filename), Binarytype) Then 
    Case BinaryType of
      SCS_32BIT_BINARY: S:= 'Win32 executable';
      SCS_DOS_BINARY  : S:= 'DOS executable';
      SCS_WOW_BINARY  : S:= 'Win16 executable';
      SCS_PIF_BINARY  : S:= 'PIF file';
      SCS_POSIX_BINARY: S:= 'POSIX executable';
      SCS_OS216_BINARY: S:= 'OS/2 16 bit executable'
    Else
      S:= 'unknown executable'
    End
Else
  S:= 'File is not an executable';
end;

{Found on the Borland Forums}                                                                                                                                                                                                                                                              Topic(   Reading the Binary Type of an Executable   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          #if(_WIN32_WINNT >= 0x0400) 
#define WM_MOUSEWHEEL                   0x020A 
#endif 
#if (_WIN32_WINNT < 0x0400) 
#define WM_MOUSELAST                    0x0209 
#else 
#define WM_MOUSELAST                    0x020A 
#endif 
 
#if(_WIN32_WINNT >= 0x0400) 
#define WHEEL_DELTA                     120     /* Value for rolling one detent */ 
#endif
#if(_WIN32_WINNT >= 0x0400) 
#define WHEEL_PAGESCROLL                (UINT_MAX) /* Scroll one page */ 
#endif
 
#define WM_PARENTNOTIFY                 0x0210 
#define MENULOOP_WINDOW                 0 
#define MENULOOP_POPUP                  1 
#define WM_ENTERMENULOOP                0x0211 

#define MOUSEEVENTF_WHEEL       0x0800 /* wheel button rolled */ 


procedure tmditted.applicationonmessage(var Msg: TMsg; var Handled: Boolean);
begin 
  if (msg.message = 52294) and (MDIchildcount > 0) then //Kollar rullknappen pO en microsoft intellimouse
    case msg.wparam of
      -120 : activemdichild.activecontrol.perform (em_linescroll,0,-1);   Topic   Intellimouse Specifications   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       
       120 : activemdichild.activecontrol.perform (em_linescroll,0,1);
    end;
end;

(*
WM_MOUSEWHEEL

The WM_MOUSEWHEEL message is sent to the focus window when the mouse wheel is
rotated. The DefWindowProc function propagates the message to the windows parent.
There should be no internal forwarding of the message, since DefWindowProc
propagates it up the parent chain until it finds a window that processes it.

WM_MOUSEWHEEL
fwKeys = LOWORD(wParam);    // key flags
zDelta = (short) HIWORD(wParam);    // wheel rotation
xPos = (short) LOWORD(lParam);    // horizontal position of pointer
yPos = (short) HIWORD(lParam);    // vertical position of pointer

Parameters

fwKeys 
  Value of the low-order word of wParam. Indicates whether various virtual keys
  are down. This parameter can be any combination of the following values:

      Value        Description
      MK_CONTROL   Set if the CTRL key is down.
      MK_LBUTTON   Set if the left mouse button is down.
      MK_MBUTTON   Set if the middle mouse button is down.
      MK_RBUTTON   Set if the right mouse button is down.
      MK_SHIFT     Set if the SHIFT key is down.



zDelta 
     The value of the high-order word of wParam. Indicates the distance that the
     wheel is rotated, expressed in multiples or divisions of WHEEL_DELTA, which is
     120. A positive value indicates that the wheel was rotated forward, away from
     the user; a negative value indicates that the wheel was rotated backward,
     toward the user.
xPos 
     Value of the low-order word of lParam. Specifies the x-coordinate of the pointer,
     relative to the upper-left corner of the screen.
yPos 
     Value of the high-order word of lParam. Specifies the y-coordinate of the pointer,
     relative to the upper-left corner of the screen. 

Remarks

The zDelta parameter will be a multiple of WHEEL_DELTA, which is set at 120. This is
the threshold for action to be taken, and one such action (for example, scrolling one
increment) should occur for each delta.

The delta was set to 120 to allow Microsoft or other vendors to build finer-resolution
wheels in the future, including perhaps a freely-rotating wheel with no notches. The
expectation is that such a device would send more messages per rotation, but with a
smaller value in each message. To support this possibility, you should either add the
incoming delta values until WHEEL_DELTA is reached (so for a given delta-rotation you
get the same response), or scroll partial lines in response to the more frequent
messages. You could also choose your scroll granularity and accumulate deltas until it
is reached.

QuickInfo

  Windows NT: Use version 4.0 and later. Implemented as ANSI and Unicode messages. 
  Header: Declared in winuser.h.
*)                                                                                                                                                                                                                                                          {Finding out the default browser}
 
Function FindClassAssignment(const fname:String):String; 
// turn shell open command for given file name 
var 
  ini : TRegIniFile; 
begin 
  ini := TRegIniFile.Create(''); 
  try 
    ini.RootKey := HKEY_CLASSES_ROOT; 
    ini.OpenKey('',FALSE); 
    Result := ini.readString(ExtractFileExt(Fname),'',''); 
    if result <> '' then begin 
      ini.OpenKey(result+'\shell\open\command',FALSE); 
      Result := ini.readString('','',''); 
    end; 
  finally 
    ini.Free; 
  end; 
end; 
 
{coded by Hector Santos}
 
                                                                                                                                                                                                                                                                                                                                                                                                                                                                  Topic   Finding Out the Default Browser   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   unit LFN_ALT;

interface

// This unit provides two functions that convert
// filenames from the long format to the 8.3
// format, and from the 8.3 format to the long
// format.

function AlternateToLFN(alternateName: String): String;
function LFNToAlternate(LongName: String): String;

implementation

uses Windows;

function AlternateToLFN(alternateName: String): String;
var 
  temp: TWIN32FindData;
  searchHandle: THandle;
begin
  searchHandle := FindFirstFile(PChar(alternateName), temp);
  if searchHandle <> ERROR_INVALID_HANDLE then
    result := String(temp.cFileName)
  else
    result := '';
  Windows.FindClose(searchHandle);
end;

function LFNToAlternate(LongName: String): String;
var 
  temp: TWIN32FindData;
  searchHandle: THandle;
begin
  searchHandle := FindFirstFile(PChar(LongName), temp);
  if searchHandle <> ERROR_INVALID_HANDLE then
    result := String(temp.cALternateFileName)
  else
    result := '';
  Windows.FindClose(searchHandle);
end;

end.

{Fo   Topic+   Converting Between Long and Short Filenames   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       und on the Borland Forums}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      {The following unit wraps the API browse window which has filters for
 Folders, Computers and Printers

Usage would be
  myString := BrowseForFolder('Select something', 
                              [boFolders,boComputers,boPrinters]);


{+------------------------------------------------------------
 | Unit ShBrowse
 |
 | Version: 1.0  Created: 05.09.1997, 12:21:00
 |               Last Modified: 05.09.1997, 12:21:00
 | Author : P. Below
 | Project: Delphi 32 examples
 | Description:
 |   This unit provides a wrapper for the Win95/NT 4.0 
 |   ShBrowseForFolder API function.
 +------------------------------------------------------------}
Unit ShBrowse;

Interface

Type
  TBrowseOption= (boFolders, boComputers, boPrinters );
  TBrowseOptions = Set of TBrowseOption;

Function BrowseForFolder(Const prompt: String; options: TBrowseOptions): String;

Implementation

Uses Windows, SysUtils, Ole2, ShlObj;

Procedure FreePidl( pidl: PItemIDList );
  Var
    allocator: IMalloc;
  B   Topic   BrowseForFolder Wrapper   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           egin
    If Succeeded(SHGetMalloc(allocator)) Then Begin
      allocator.Free(pidl);
      allocator.Release;
    End;
  End;

{+------------------------------------------------------------
 | Function BrowseForFolder
 |
 | Parameters:
 |  prompt : text that should appear above the list of folders
 |  options: defines what should be offered in the list as 
 |           selectable. Other items will appear in the list but
 |           selecting them will not enable the OK button.
 | Returns:
 |  The name of the selected folder, printer etc., if successful,
 |  otherwise an empty string.
 | Call method:
 |  static
 | Description:
 |  This function is a rather thin wrapper around the SHBrowseForFolder
 |  API function. It displays the standard Explorer dialog for 
 |  selecting folders and other stuff.
 | Error Conditions:
 |  Errors will cause the returned string to be empty.
 |
 |Created: 05.09.1997 12:22:49 by P. Below
 +------------------------------------------------------------}
Function BrowseForFolder(Const prompt: String; options: TBrowseOptions):String;
  Const { Translates our options to BIF_ constants. }
    OpValues : Array [TBrowseOption] of UINT =
      ( BIF_RETURNONLYFSDIRS, BIF_BROWSEFORCOMPUTER,BIF_BROWSEFORPRINTER);
  Var
    bi: TBrowseInfo;
    nameBuf: Array [0..MAX_PATH] of Char;
    pidl: pItemIDList;
    index: TBrowseOption;
  Begin
    Result := EmptyStr;
    With bi Do Begin
      hwndOwner:= GetActiveWindow;
      pidlRoot:= Nil;
      pszDisplayName:= @namebuf;
      namebuf[0]:= #0;
      lpszTitle:= PChar( prompt );
      ulFlags:= 0;
      For index := Low(index) To High(index) Do
        If index In options Then
          ulFlags := ulFlags or OpValues[index];
      lpfn:= Nil;
      lParam:= 0;
      iImage:= 0;
    End;
    pidl := ShBrowseForFolder( bi );
    If pidl <> Nil Then Begin
      If SHGetPathFromIDList(pidl, namebuf) Then
        Result := StrPas(namebuf);
      FreePidl( pidl );
    End Else { No idea if this makes sense, probably not <g>. }
      Result := StrPas(bi.pszDisplayName);
  End;

end.

{Code written by Peter Below (TeamB)}


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     // See if a mouse is available on the machine
function IsMouseThere: Boolean;
begin
  Result := (GetSystemMetrics(SM_MOUSEPRESENT) <> 0);
end;

// From Roy Lavers
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         Topic   Is a Mouse Present?   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               // Fake a press of the PrintScrn key
procedure PrintScreen;
begin
  keybd_event(VK_MENU,     MapVirtualkey(VK_MENU, 0 ),   0, 0);
  keybd_event(VK_SNAPSHOT, MapVirtualKey(VK_SNAPSHOT, 0), 0, 0);
  keybd_event(VK_SNAPSHOT, MapVirtualKey(VK_SNAPSHOT, 0), KEYEVENTF_KEYUP, 0);
  keybd_event(VK_MENU,     MapVirtualkey(VK_MENU, 0), KEYEVENTF_KEYUP, 0);
end;

// From Roy Lavers
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   Topic   Fake the PrintScrn Key   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            // Determine whether the current user has admin rights under NT
--------------------------
const
  SECURITY_NT_AUTHORITY: TSIDIdentifierAuthority = (Value: (0, 0, 0, 0, 0, 5));

const
  SECURITY_BUILTIN_DOMAIN_RID = $00000020;
  DOMAIN_ALIAS_RID_ADMINS     = $00000220;

function IsAdmin: Boolean;
var
  hAccessToken: THandle;
  ptgGroups: PTokenGroups;
  dwInfoBufferSize: DWORD;
  psidAdministrators: PSID;
  x: Integer;
  bSuccess: BOOL;
begin
  Result := False;

  bSuccess := OpenThreadToken( GetCurrentThread, TOKEN_QUERY, True, hAccessToken );

  if not bSuccess then
    if ( GetLastError = ERROR_NO_TOKEN ) then
      bSuccess := OpenProcessToken( GetCurrentProcess, TOKEN_QUERY, hAccessToken);

  if bSuccess then
  begin
    GetMem(ptgGroups, 1024);

    bSuccess :=
       GetTokenInformation( hAccessToken,
                            TokenGroups,
                            ptgGroups,
                            1024,
                            dwInfoBufferSize );

       Topic   Administrator Rights under NT   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     CloseHandle(hAccessToken);

    if bSuccess then
    begin
      AllocateAndInitializeSid( SECURITY_NT_AUTHORITY,
                                2,
                                SECURITY_BUILTIN_DOMAIN_RID,
                                DOMAIN_ALIAS_RID_ADMINS,
                                0, 0, 0, 0, 0, 0,
                                psidAdministrators );

      {$R-}
      for x := 0 to ptgGroups.GroupCount - 1 do
        if EqualSid(psidAdministrators, ptgGroups.Groups[x].Sid) then
        begin
          Result := True;
          Break;
        end;
      {$R+}

      FreeSid(psidAdministrators);
    end;

    FreeMem(ptgGroups);
  end;
end;

// From Roy Lavers
                                                                                                                                                                                                                                                                                                                      // WIN98.  Microsoft kindly disabled SetForegroundWindow in Win98.  This is
// a workaround function:
function ForceForegroundWindow(hWnd: THandle): Boolean;
const
     SPI_GETFOREGROUNDLOCKTIMEOUT = $2000;
     SPI_SETFOREGROUNDLOCKTIMEOUT = $2001;
var
   Timeout: DWORD;
begin
  if (( Win32Platform = VER_PLATFORM_WIN32_NT) and (Win32MajorVersion > 4 )) or
      ((Win32Platform =VER_PLATFORM_WIN32_WINDOWS) and ((Win32MajorVersion > 4 ) or
      ((Win32MajorVersion = 4) and (Win32MinorVersion > 0)))) then
  begin
    SystemParametersInfo(SPI_GETFOREGROUNDLOCKTIMEOUT, 0, @Timeout, 0);
    SystemParametersInfo(SPI_SETFOREGROUNDLOCKTIMEOUT, 0, TObject(0), SPIF_SENDCHANGE);
    Result := SetForegroundWindow(hWnd);
    SystemParametersInfo(SPI_SETFOREGROUNDLOCKTIMEOUT, 0, TObject(Timeout), SPIF_SENDCHANGE);
  end
  else
    Result := SetForegroundWindow(hWnd);
end;

// From Roy Lavers (from a newsgroup posting)
                                                                                     Topic%   Using SetForegroundWindow under Win98   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              Getting the TaskBar Handle    �   v    Getting the TaskBar Handle   �   <   " Seperate Taskbar Buttons for Forms    �   w  " Seperate Taskbar Buttons for Forms   �   D    Hiding a Form from the TaskBar    �   �   Hiding a Form from the TaskBar   �   @    Hide or Show the TaskBar    �   �   Hide or Show the TaskBar   �   :   $ Using Windows95 Shell File Functions    �   �  $ Using Windows95 Shell File Functions   �   F   ' Getting File Information from the Shell    �   �  ' Getting File Information from the Shell   �   I   ! Deleting Files to the Recycle Bin    �   l  ! Deleting Files to the Recycle Bin   �   C     Creating and Resolving Shortcuts    �   M    Creating and Resolving Shortcuts   �   B    Creating Desktop Icons    �   �
   Creating Desktop Icons   �   8                                                                                                                                                                                                                                  Topic   Shell Stuff                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        {How to obtain Windows Taskbar handle}

  hTaskbar := FindWindow('Shell_TrayWnd', Nil ); 

{Peter Below (TeamB)}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             Topic   Getting the TaskBar Handle   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        {To show other forms of a project in the Windows task bar try overriding
 the following method to each form you want visible :}

Procedure TForm1.CreateParams(Var params: TCreateParams );
Begin
  inherited;
  Params.WndParent := GetDesktopWindow;
  params.exstyle := params.exstyle and not WS_EX_TOOLWINDOW or WS_EX_APPWINDOW;
end;

{coded by Peter Below (TeamB)}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Topic"   Seperate Taskbar Buttons for Forms   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                {To hide a form from showing up in the Win 95 task bar

Make your DPR look like this --}

program TrayProj;

uses
  Forms,
  ...
  
{$R *.RES}

begin
  Application.ShowMainForm := False;
  Application.CreateForm(TForm1, Form1);
  Application.Run;
end.

{and/or your Main form's FormCreate look like this --}

procedure TForm1.FormCreate(Sender: TObject);
begin
  ShowWindow( Application.Handle, SW_HIDE );
  SetWindowLong( Application.Handle, GWL_EXSTYLE,
                 GetWindowLong(Application.Handle, GWL_EXSTYLE) or
                 WS_EX_TOOLWINDOW and not WS_EX_APPWINDOW);
  ShowWindow( Application.Handle, SW_SHOW );
end;

{both may be redundant, but it gets the job done}

{Found on the borland forums}                                                                                                                                                                                                                                                                                          Topic   Hiding a Form from the TaskBar   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    {To hide or show the Windows 95 taskbar programmatically from your Delphi
 application call one of these functions}

procedure hideTaskbar; 
var 
  wndHandle : THandle; 
  wndClass : array[0..50] of Char; 
begin 
  StrPCopy(@wndClass[0], 'Shell_TrayWnd'); 
  wndHandle := FindWindow(@wndClass[0], nil); 
  ShowWindow(wndHandle, SW_HIDE); // This hides the taskbar 
end; 

procedure showTaskbar; 
var
  wndHandle : THandle; 
  wndClass : array[0..50] of Char; 
begin 
 StrPCopy(@wndClass[0], 'Shell_TrayWnd'); 
 wndHandle := FindWindow(@wndClass[0], nil); 
 ShowWindow(wndHandle, SW_RESTORE); // This restores the taskbar 
end;
                                                                                                                                                                                                                                                                                                                                                                                          Topic   Hide or Show the TaskBar   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          uses shellapi; // don't forget

procedure shelloperate (a:integer;b,c:tstrings); // does the a-specified shell-file-operation
{ a can be fo_move, fo_delete, fo_copy or fo_rename
  b are the source-filenames
  c are the destination-filenames}
var 
  shfileopstruct : tshfileopstruct;
  fname,dest : string;
  ct : integer;
begin
  // let us create the source-filename
  // all filenames in one string, divided by a #0 and on the end another #0, too
  fname := '';
  if b.count > 0 then 
    for ct := 0 to pred(b.count) do 
      fname := fname+b[ct]+#0;
  fname := fname+#0;

  // get the destination filenasme
  dest := getcurrentdir; // default
  if a= fo_rename then 
    dest:=inputbox ('rename file','enter new filename',fname);

  fillchar(shfileopstruct,sizeof(tshfileopstruct),0);
  with shfileopstruct do begin
    wnd      := form1.handle;      // set this to the calling window's handle
    wfunc    := a;                 // here set the desired shell-function
    pfrom    := pchar(fn   Topic$   Using Windows95 Shell File Functions   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              ame);      // these are the source-filenames
    pto      := pchar(dest); // destination
    //fflags     // these are flags for the shelloperation (look to the help)
    //fanyoperationsaborted   // is true if user cancelled the operation
    //hnamemappings    // filemapping-pointer
    //lpszprogresstitle       // if no dialog-boxes (fflags) then show this string (?)
  end;

  // and now do the operation
  shfileoperation(shfileopstruct);
end;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  {How to receive shell information about a file}

uses shellapi; //that unit is necessary

procedure getshellinfo(a:tfilename;var name,typ:string;var icon:ticon;var attr:integer);
var 
  info : tshfileinfo;
begin
     // get shell-info about the specified file (in "a")
     {parameters :
          name : receives the explorer-style filename (e.g. without extension)
          typ  : receives the explorer-style typename of the file (e.g. "anwendung" for an exe in german win95)
          icon : will content the icon which is shown in the explorer for that file (this is slower than extracticon !!!!)
          attr : gets the explorer-attributes of the file, these are (ripped from win32-help):
               sfgao_cancopy the specified file objects or folders can be copied (same value as the dropeffect_copy flag).
               sfgao_candelete the specified file objects or folders can be deleted.
               sfgao_canlink it is possible to create shortcuts for the specified file objects or folde   Topic'   Getting File Information from the Shell   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           rs (same value as the dropeffect_link flag).
               sfgao_canmove the specified file objects or folders can be moved (same value as the dropeffect_move flag).
               sfgao_canrename the specified file objects or folders can be renamed.
               sfgao_capabilitymask mask for the capability flags.
               sfgao_droptarget the specified file objects or folders are drop targets.
               sfgao_haspropsheet the specified file objects or folders have property sheets.

               a file object's display attributes may include zero or more of the following values:

               sfgao_displayattrmask mask for the display attributes.
               sfgao_ghosted the specified file objects or folders should be displayed using a ghosted icon.
               sfgao_link the specified file objects are shortcuts.
               sfgao_readonly the specified file objects or folders are read-only.
               sfgao_share the specified folders are shared.

               a file object's contents flags may include zero or more of the following values:

               sfgao_contentsmask mask for the contents attributes.
               sfgao_hassubfolder the specified folders have subfolders (and are, therefore, expandable in the left pane of windows explorer).

               a file object may have zero or more of the following miscellaneous attributes:

               sfgao_filesystem the specified folders or file objects are part of the file system (that is, they are files, directories, or root directories).
               sfgao_filesysancestor the specified folders contain one or more file system folders.
               sfgao_folder the specified items are folders.
               sfgao_removable the specified file objects or folders are on removable media.
               sfgao_validate validate cached information.}


     fillchar(info,sizeof(tshfileinfo),0);
     shgetfileinfo(pchar(a),0,info,sizeof(info),shgfi_displayname or shgfi_typename or
     shgfi_icon or shgfi_attributes);
     with info do begin
          name := szdisplayname;
          typ  := sztypename;
          icon.handle := hicon;
          attr        := dwattributes;
     end;
end;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  {example of use
   ToRecycle('c:\Yow.txt', Handle);}

uses ShellAPI;

procedure ToRecycle(aFileName: String; hnd: THandle);
var
  SHF: TSHFileOpStruct;
begin
  with SHF do begin
    Wnd := Hnd;
    wFunc := FO_DELETE;
    pFrom := PChar(aFileName);
    fFlags := FOF_ALLOWUNDO;
  end;
  SHFileOperation(SHF);
end;

{by Xavier Pacheco (TeamB)}

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       Topic!   Deleting Files to the Recycle Bin   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 (*The CreateLink function in the following example creates a shortcut.
The parameters include a pointer to the name of the file to link to,
a pointer to the name of the shortcut that you are creating, and a

pointer to the description of the link. The description consists of
the string, "Shortcut to filename," where filename is the name of the
file to link to.

Because CreateLink calls the CoCreateInstance function, it is assumed
that the CoInitialize function has already been called. CreateLink
uses the IPersistFile interface to save the shortcut and the IShellLink
interface to store the filename and description.

 CreateLink - uses the shell's IShellLink and IPersistFile interfaces
 to create and store a shortcut to the specified object.  Returns the
 result of calling the member functions of the interfaces.

 lpszPathObj  - address of a buffer containing the path of the object
 lpszPathLink - address of a buffer containing the path where the
                shell link is to be stored
 l   Topic    Creating and Resolving Shortcuts   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  pszDesc     - address of a buffer containing the description of the
                shell link*)

unit ShellUtl;

interface

uses Windows,Ole2;

function CreateLink(lpszPathObj,lpszPathLink,lpszDesc:String):HResult;
function ResolveIt(Wnd:HWND; lpszLinkFile:String):String;

implementation

uses SysUtils, ShellAPI, ShellObj;

function CreateLink(lpszPathObj,lpszPathLink,lpszDesc:string):HResult;

var
  hRes: HRESULT;
  psl: IShellLink;
  ppf: IPersistFile;
  wsz: PWideChar;

begin
    GetMem(wsz,MAX_PATH*2);
    try
    { Get a pointer to the IShellLink interface. }
    hres := CoCreateInstance(CLSID_ShellLink, nil,
                            CLSCTX_INPROC_SERVER, IID_IShellLink, psl);
    if SUCCEEDED(hres) then
       begin
       { Set the path to the shortcut target, and add the description.  }
       psl.SetPath(@lpszPathObj[1]);
       psl.SetDescription(@lpszDesc[1]);

       { Query IShellLink for the IPersistFile interface for saving the 
         shortcut in persistent storage. }
       if SUCCEEDED(psl.QueryInterface(IID_IPersistFile,ppf)) then
         begin
         { Ensure that the string is ANSI. }
         MultiByteToWideChar(CP_ACP, 0, @lpszPathLink[1],-1,wsz,MAX_PATH);
         { Save the link by calling IPersistFile::Save. }
         hres := ppf.Save(wsz,TRUE);
         ppf.Release;
         end;
       psl.Release;

       end;
    Result := hres;
 finally
    FreeMem(wsz,MAX_PATH*2);
    end;
end;


function ResolveIt(Wnd:HWND; lpszLinkFile:String):String;
var
  hres:HRESULT;
  psl:IShellLink;
  szGotPath: array[0..MAX_PATH-1] of char;
  szDescription: array[0..MAX_PATH-1] of char;
  wfd: TWin32FindData;
  ppf: IPersistFile;
  wsz: array[0..MAX_PATH-1] of WideChar;

begin
  Result := ''; { assume failure  }
  { Get a pointer to the IShellLink interface. }
  hres := CoCreateInstance(CLSID_ShellLink, nil,
          CLSCTX_INPROC_SERVER, IID_IShellLink, psl);
  if (SUCCEEDED(hres)) then begin
    { Get a pointer to the IPersistFile interface. }
    hres := psl.QueryInterface(IID_IPersistFile,ppf);
    if (SUCCEEDED(hres)) then begin
      { Ensure that the string is Unicode. }
      MultiByteToWideChar(CP_ACP, 0, @lpszLinkFile[1], -1, wsz, MAX_PATH);
      { Load the shortcut. }
      hres := ppf.Load(wsz, STGM_READ);
      if (SUCCEEDED(hres)) then begin
        { Resolve the link. }
        hres := psl.Resolve(wnd,SLR_ANY_MATCH);
        if (SUCCEEDED(hres)) then begin
          { Get the path to the link target. }
          hres := psl.GetPath(szGotPath,MAX_PATH,wfd,SLGP_SHORTPATH);
          if not SUCCEEDED(hres) then 
            exit;
          { Get the description of the target. }
          hres := psl.GetDescription(szDescription, MAX_PATH);
          if not SUCCEEDED(hres) then 
            exit;
          Result := StrPas(szGotPath)+'|'+StrPas(szDescription);
        end;
      end;
      { Release the pointer to the IPersistFile interface. }
      ppf.Release;
    end;
    { Release the pointer to the IShellLink interface. }
    psl.Release;
  end;
end;

end.
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   {how to create an Icon for an application on a Windows desktop}

Yep try this code... Hope this helps...

Uses
{$IFDEF  VER90} OleAuto, {$ENDIF}
{$IFDEF VER100} ComObj,{$ENDIF}
  ShlObj; //ShellCom in early versions of Delphi

{----------------------------------------------------------------------------
  _SHORTCUTS_
  Create a Windows Shortcut
----------------------------------------------------------------------------}
procedure _CreateShellLink(sDesc,sLinkToFilename,sStartIn,sArguments,
                           sIconPath,sLinkName: String;
                           iIconIndex: Integer);
var
  sl:  IShellLink;
  ppf: IPersistFile;
  wcLinkName: array[0..Max_Path] of WideChar;
begin
  OleCheck(CoInitialize(Nil));
  OleCheck(
{$IFDEF VER90}  CoCreateInstance(OLE2.TGUID(CLSID_ShellLink), nil,
            CLSCTX_INPROC_SERVER, OLE2.TGUID(IID_IShellLink), sl)); {$ENDIF}
{$IFDEF VER100} CoCreateInstance(OLE2.TGUID(CLSID_ShellLink), nil,
            CLSCTX_INPROC_SERVER, OLE2.TGUID(IID   Topic   Creating Desktop Icons   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            _IShellLinkA), sl));{$ENDIF}
  OleCheck(
{$IFDEF VER90}  sl.QueryInterface(OLE2.TGUID(IID_IPersistFile), ppf));{$ENDIF}
{$IFDEF VER100}  sl.QueryInterface(System.TGUID(IID_IPersistFile), ppf));{$ENDIF}
  OleCheck(sl.SetDescription(PChar(sDesc)));
  OleCheck(sl.SetPath(PChar(sLinkToFilename)));
  OleCheck(sl.SetWorkingDirectory(PChar(sStartIn)));
  OleCheck(sl.SetArguments(PChar(sArguments)));
  OleCheck(sl.SetIconLocation(PChar(sIconPath), iIconIndex));
  MultiByteToWideChar(CP_ACP, 0, PChar(sLinkName), -1, wcLinkName, MAX_PATH);
  OleCheck(ppf.Save(wcLinkName, true));
  CoUninitialize;
end; {_CreateShellLink}


procedure GetFolderPath(var sPath: String; iFolder: Integer);
var
  iID: PItemIDList;
  szPath: PChar;
begin
  sPath := '';  //error
  szPath := StrAlloc( MAX_PATH );
  if SHGetSpecialFolderLocation(Application.handle, iFolder, iID) = NOERROR
then
  begin
    SHgetPathFromIDList(iID, szPath);
    sPath := szPath;
  end
end;

{
  Call this to create a file shortcut (ShellLink)
    filename: Full path to file to link to
    Location: see help on SHGetSpecialFolderLocation()
      CSIDL_DESKTOP, CSIDL_PROGRAMS, CSIDL_STARTMENU
  filename: Target Filename to Link to
  LinkName: Name that will appear under the link icon eg. 'Shortcut to XXXX'
  Arguments: Command line auguments
}
procedure CreateShellLink(filename, LinkName, Arguments: String;  Location:
Word);
var sDesktopPath, sLinkPath: String;
begin
  GetFolderPath(sDesktopPath, Location);
  sLinkPath := sDesktopPath + '\' + LinkName + '.lnk';
  _CreateShellLink('', filename, ExtractFilePath(filename), Arguments, filename, sLinkPath, 0);
end;
                                                                                                                                                                                                                                                                                                                                                                                       Component Enhancements   �   �   Component Enhancements   �   '    Form Level Stuff   �   J   Form Level Stuff   �   !                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  Topic   VCL                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 Tree Structure to Text    �   �   Tree Structure to Text   �   8    ToolTip Font Hint Properties    �   I   ToolTip Font Hint Properties   �   >    Dragging Controls at Runtime    �   L   Dragging Controls at Runtime   �   >    Transparent Canvas Text    �      Transparent Canvas Text   �   9   % Storing Component States to INI Files    �   f  % Storing Component States to INI Files   �   G   " Listview Sorting via Column Clicks    �   �  " Listview Sorting via Column Clicks   �   D    Memo Printing    �   <   Memo Printing   �   /    RichEdit Flicker Reduction    �   �    RichEdit Flicker Reduction   �   <   , Making a Component Responsive at Design-Time    �   6  , Making a Component Responsive at Design-Time   �   N    Page Control Accellerator Keys    �   �
   Page Control Accellerator Keys   �   @    Creating Transparent Controls    �   s   Creating Transparent Controls   �   ?   $ Opening - Manipulating Menus in Code    �   6  $ Opening - Manipulating Menus in Code   Topic   Component Enhancements                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             unit Tree2Text;

interface

uses Classes, ComCtrls, Outline;

{
  Generic tree to text conversion. Use non-proportional font (ie. Terminal,
  for IBM character set).

  Supplied, for you convenience, with TreeView and Outline functions (which
  also act as examples for adapting to your own favourite tree component).

  Can be fitted out to other trees by deriving five method node wrapper
  from TttTreeNode (see below).

  Componentising what should boil down to a single procedure call, would be
  overkill.

  Version .1 - Feb 2000

  Written by Greg Lorriman <greg@lorriman.demon.co.uk> in feb 2000
}

type
  TttEnumCharacterSet = (csDownLine, csSiblingJoin, csEndPoint);
  TttCharacterSet = array[Low(TttEnumCharacterSet)..High(TttEnumCharacterSet)] of string;

 // Node wrapper: Do not free real node in your implementation.
 // Create a private field to hold ref to real node
 // and write appropriate constructor. Create in "get" methods.
 // See supplied examples for TTreeNode and    Topic   Tree Structure to Text   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            TOutlineNode
  TttTreeNode = class
  public
    function HasNextSibling: Boolean; virtual; abstract;
    function GetNextSibling: TttTreeNode; virtual; abstract;
    function HasChildren: Boolean; virtual; abstract;
    function GetFirstChild: TttTreeNode; virtual; abstract;
    function GetText: string; virtual; abstract;
  end;

// Generic - you can call this via a wrapper class, see examples below
procedure Tree2Strings(Node: TttTreeNode; Strings: TStrings; charSet: TttCharacterSet);

procedure Tree2StringsTreeView(Node: TTreeNode; Strings: TStrings; CharSet: TttCharacterSet);

// Due to TOutlineNode limitations TOutline object also needs to be supplied
procedure Tree2StringsOutline(Node: TOutlineNode; Outline: TOutline; Strings: TStrings; CharSet: TttCharacterSet);

// This lot aids code-insight as well as source readability.
// Switch between character sets, or supply your own.
// These include spacing (#32) as it seems to look better.
const
  IBMcharset: TttCharacterSet = (#179#32, #195#32, #192#32);
  ASCIIcharset1: TttCharacterSet = ('| ', '\- ', '\- ');
  ASCIIcharset2: TttCharacterSet = ('| ', '\_ ', '\_ ');
  ASCIIcharset3: TttCharacterSet = ('| ', '+ ', '- ');

implementation

type
  TTreeViewNode = class(TttTreeNode)
  private
    FNode: TTreeNode;
  public
    constructor Create(Node: TTreeNode);
    function HasNextSibling: Boolean; override;
    function GetNextSibling: TttTreeNode; override;
    function HasChildren: Boolean; override;
    function GetFirstChild: TttTreeNode; override;
    function GetText: string; override;
  end;

  TOutlineTreeNode = class(TttTreeNode)
  private
    FNode: TOutlineNode;
    FOutline: TOutline;
  public
    constructor Create(Outline: TOutline; Node: TOutlineNode);
    function HasNextSibling: Boolean; override;
    function GetNextSibling: TttTreeNode; override;
    function HasChildren: Boolean; override;
    function GetFirstChild: TttTreeNode; override;
    function GetText: string; override;
  end;

procedure tree2stringsTreeView(Node: TTreeNode; Strings: TStrings; CharSet: TttCharacterSet);
begin
  Tree2Strings(TTreeViewNode.Create(Node), Strings, CharSet);
end;

procedure tree2StringsOutline(Node: TOutlineNode; Outline: TOutline; Strings: TStrings; charSet: TttCharacterSet);
begin
  Tree2Strings(TOutlineTreeNode.Create(Outline, Node), Strings, CharSet);
end;

procedure Tree2StringsRecurse(Node: TttTreeNode; Strings: TStrings; CharSet: TttCharacterSet; NodesToDo: Tlist; Depth: Integer); forward;

procedure PrintTreeLevel(Node: TttTreeNode; Strings: TStrings; CharSet: TttCharacterSet; NodesToDo: Tlist; Depth: Integer);
var
  s, str: string;
  i: Integer;
begin
  for i := 0 to Depth - 1 do
  begin
    if NodesToDo[i] <> nil then
      s := CharSet[csDownline]
    else
      s := #32;
    str := str + s;
  end;
  if Node.HasNextSibling then
    str := str + CharSet[csSiblingJoin] + Node.GetText
  else
    str := str + CharSet[csEndPoint] + Node.GetText;
  Strings.Add(str);
end;

procedure updateToDoList(Node: TttTreeNode; nodesToDo: Tlist; depth: Integer);
begin
  NodesToDo.Count := Depth + 1;
  if Node.HasNextSibling then
    NodesToDo[Depth] := Pointer(1)
  else
    NodesToDo[Depth] := nil;
end;

// Contortion in here (sibling) needed to support auto-release of node wrappers
procedure RecurseChildren(Node: TttTreeNode; Strings: TStrings; CharSet: TttCharacterSet; nodesToDo: Tlist; depth: Integer);
var
  Child, Sibling: TttTreeNode;
  i: Integer;
begin
  if Node.HasChildren then
  begin
    Child := Node.GetFirstChild;
    repeat
      if Child.HasNextSibling then
        Sibling := Child.GetNextSibling
      else
        Sibling := nil;
      Tree2StringsRecurse(Child, Strings, CharSet, NodesToDo, Depth + 1);
      Child := Sibling;
    until Child = nil;
  end;
end;

procedure tree2stringsRecurse(Node: TttTreeNode; Strings: TStrings; charSet: TttCharacterSet; nodesToDo: Tlist; depth: Integer);
begin
  PrintTreeLevel(Node, Strings, CharSet, NodesToDo, Depth);
  UpDateToDoList(Node, NodesToDo, Depth);
  RecurseChildren(Node, Strings, CharSet, NodesToDo, Depth);
   // Freeing the wrapper, not the real node.
  Node.Free;
end;

procedure tree2strings(Node: TttTreeNode; Strings: TStrings; CharSet: TttCharacterSet);
var
  NodesToDo: TList;
begin
  NodesToDo := TList.Create;
  try
    NodesTodo.Count := 0;
    Tree2StringsRecurse(Node, Strings, CharSet, NodesToDo, 0);
  finally
    NodesToDo.Free;
  end;
end;

{ TTreeViewNode }

constructor TTreeViewNode.Create(Node: TTreeNode);
begin
  FNode := Node;
end;

function TTreeViewNode.getFirstChild: TttTreeNode;
begin
  Result := TTreeViewNode.Create(FNode.GetFirstChild);
end;

function TTreeViewNode.getNextSibling: TttTreeNode;
begin
  Result := TTreeViewNode.Create(FNode.GetNextSibling);
end;

function TTreeViewNode.GetText: string;
begin
  Result := FNode.Text;
end;

function TTreeViewNode.HasChildren: Boolean;
begin
  Result := FNode.HasChildren;
end;

function TTreeViewNode.HasNextSibling: Boolean;
begin
  Result := FNode.GetNextsibling <> nil;
end;

{ TOutlineTreeNode }

constructor TOutlineTreeNode.Create(Outline: TOutline; Node: TOutlineNode);
begin
  FNode := Node;
  FOutline := Outline;
end;

function TOutlineTreeNode.GetFirstChild: TttTreeNode;
begin
  Result := TOutlineTreeNode.Create(FOutline, FOutline.Items[Fnode.getFirstChild]);
end;

function TOutlineTreeNode.GetNextSibling: TttTreeNode;
begin
  Result := TOutlineTreeNode.Create(FOutline, FOutline.Items[Fnode.Parent.getNextChild(Fnode.Index)]);
end;

function TOutlineTreeNode.GetText: string;
begin
  Result := FNode.Text;
end;

function TOutlineTreeNode.HasChildren: Boolean;
begin
  Result := FNode.HasItems;
end;

function TOutlineTreeNode.HasNextSibling: Boolean;
begin
  if FNode.Parent = nil then
    Result := False
  else
    Result := Fnode.Parent.GetNextChild(Fnode.Index) <> -1;
end;

end.

                                                                     {Why Borland did not make the hintwindow font public is one of the great 
 mysteries of all time.}

Type
  TMyHintWindow = Class (THintWindow)
    Constructor Create (AOwner: TComponent); override;
  end;

Constructor TMyHintWindow.Create (AOwner: TComponent); 
begin
  Inherited Create (AOwner);
  Canvas.Font.Name := 'Courier New';
  Canvas.Font.Size := 72;
end;

procedure TForm1.FormCreate(Sender: TObject);
begin
  Application.ShowHint := false;
  HintWindowClass := TMyHintWindow;
  Application.ShowHint := True;
end;

{written by Scott Samet / TeamB}


                                                                                                                                                                                                                                                                                                                                                                                                                                                          Topic   ToolTip Font Hint Properties   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      {Here is a way to set up a form to support dragging components around
 on it at run time.  Each control that you want to move in this fashion
 should have its mouse down, mouse move, and mouse up events set to
 ControlMouseDown, ControlMouseMove and ControlMouseUp, respectively}

unit DragAroundControlsExample;

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms,Dialogs,
  StdCtrls, ExtCtrls, ComCtrls, DBTables, DB;

type
  TForm1 = class(TForm)
    procedure ControlMouseDown(Sender: TObject; Button: TMouseButton;
                               Shift: TShiftState; X, Y: Integer);
    procedure ControlMouseMove(Sender: TObject; Shift: TShiftState; 
                               X,Y: Integer);
    procedure ControlMouseUp(Sender: TObject; Button: TMouseButton;
                             Shift: TShiftState; X, Y: Integer);
  private
    { Private declarations }
    downX, downY: Integer;
    dragging: Boolean;
  public
    { Public declarations }
  end   Topic   Dragging Controls at Runtime   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      ;

var
  Form1: TForm1;

implementation

{$R *.DFM}

Type
  TCracker = Class(TControl);
  { Needed since TControl.MouseCapture is protected 
    by declaring a descendant class we can typecast the control
    to this and access its protected methods with in this unit.}

{ Control event handlers are attached to both memo and image mouse
  events. }
procedure TForm1.ControlMouseDown(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
  downX:= X;
  downY:= Y;
  dragging := True;
  TCracker(Sender).MouseCapture := True;
end;

procedure TForm1.ControlMouseMove(Sender: TObject; Shift: TShiftState; X,
  Y: Integer);
begin
  If dragging Then with Sender As TControl Do Begin
    Left := X-downX+Left;
    Top  := Y-downY+Top;
  End;
end;

procedure TForm1.ControlMouseUp(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
  If dragging then Begin
    dragging := False;
    TCracker(Sender).MouseCapture := False;
  End;
end;

initialization
end.


{code written by Peter Below (TeamB)}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    {The following snippet has text drawn as opaque and transparent}

procedure TForm1.Button1Click(Sender: TObject);
var
  OldBkMode : integer;
begin
  with Form1.Canvas do begin
    Brush.Color := clRed;
    FillRect(Rect(0, 0, 100, 100));
    Brush.Color := clBlue;
    TextOut(10, 20, 'Not Transparent!');
    OldBkMode := SetBkMode(Handle, TRANSPARENT); // returns what bkmode was
    TextOut(10, 50, 'Transparent!');
    SetBkMode(Handle, OldBkMode); // restores what bkmode was
  end;
end;

{Code written by Joe C. Hecht}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      Topic   Transparent Canvas Text   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           {**************************************************************
 *  TExtIniF extends Delphi's TIniFile to simplify saving a   *
 *  components state to an INI-File. After creation you can   *
 *  register any number of components and save and retrieve   *
 *  their settings with just one call to StoreObjectStates.   *
 *                                                            *
 **************************************************************}

unit extINIF;

interface

uses
  {unfortunately we need to include a units of classes
    that we want to be able to store}
  IniFiles, Classes, Forms, StdCtrls, FileCtrl, Menus,
  SysUtils, TabNotBK;

type
  EExtIniFError = class(Exception);
  TExtIniF = class(TIniFile)
  private
    {store all objects states before TExtIniF is destroyed}
    FAutoStore: boolean;
    {list of all registered objects}
    FRegObjects: TStringList;
    {Name of [section] where values are stored}
    FIniSection: String;
  public
    constructor create(IniFNa   Topic%   Storing Component States to INI Files   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             me: TFileName);
    destructor destroy; override;
    {find the ini section for a registered object}
    function GetIniSection(obj: TObject): string;
    {Add a component to the list of objects}
    procedure RegisterObject(obj: TObject; INISection: string);
    {Remove a component to the list of objects}
    procedure UnRegisterObject(obj: TObject; INISection: string);
    {Retrieve the setting of a single Object}
    procedure ReStoreObjectState(obj: TObject; INISection: string);
    {Restore states of all registered objects}
    procedure RestoreObjectStates;
    {Restore states of all registered objects}
    procedure StoreObjectState(obj: TObject; INISection: string);
    {Store state of a single object}
    procedure StoreObjectStates;
    {Store states of all registered objects}
  published
    property AutoStore: boolean read FAutoStore write FAutoStore;
    property IniSection: string read FIniSection write FIniSection;
  end;

implementation

function ExtractFileBaseName(FullName: string): string;
{return the whole path without the extension}
var
  endPos: integer;
begin
  result := FullName;
  endPos := length(result);
  repeat
    dec(endPos);
  until result[endPos] = '.';
  delete(result,endPos,maxInt);
end;

constructor TExtIniF.create(IniFName: TFileName);
begin
  {if you don't pass your own name for the ini-File, it will be the name
   of your exe-file with the extension '*.INI'}
  if (IniFName = '') then
    IniFName:= ExtractFileBaseName(application.exename)+'.ini';
  inherited create(IniFName);
  FRegObjects:= TStringList.Create;
  FIniSection:= 'Options';
end;

destructor TExtIniF.Destroy;
begin
  {If AutoStore is set, values are stored
   before TExtIniF-Object is destroyed}
  if FAutoStore then StoreObjectStates;
  FRegObjects.destroy;
  inherited destroy;
end;

{ find the section string to a registered object
   if not registered or section string is empty
   return default value}
function TExtIniF.GetIniSection(obj: TObject): string;
var
  index: integer;
begin
  index:= FRegObjects.indexOfObject(obj);
  if ( index > -1 ) then
    begin
      result:= FRegObjects.strings[index];
      if result = '' then
        result:= FIniSection;
    end
  else
    result:= FIniSection;
end; {GetIniSection}

{ Add an object to the list of monitored objects. If you pass an empty string
   for INISection, the default value will apply and no name will be stored}
procedure TExtIniF.RegisterObject(obj: TObject; INISection: string);
begin
  {check if object is already registered}
  if (FRegObjects.indexOfObject(obj) = -1) then
    FRegObjects.addObject(INISection,obj);
end;

{ Remove an object from the list of monitored objects.}
procedure TExtIniF.UnRegisterObject(obj: TObject; INISection: string);
var
  index: integer;
begin
  index:= FRegObjects.indexOfObject(obj);
  if (index > -1) then 
    FRegObjects.delete(index);
end;

{ Restores the name of an object from the INI-File
   Note: When there is no entry in the INI-File, the object's value
          is not changed.}
procedure TExtIniF.ReStoreObjectState(obj: TObject; INISection: string);
var
  strBuf: string;
begin
  if ( INISection = '' ) then INISection:= FIniSection;
  {the next lines check for the type of object and
   restore whatever property we would like to store of that object
   if you make changes here you will need to make changes in
   StoreObjectState as well!!!}
  if (obj.classInfo <> nil ) then 
    begin
      if (obj is TCheckBox) then with (obj as TCheckBox) do 
        {Checkboxes: restore checked state}
        checked:= ReadBool(INISection,Name,checked)
      else if (obj is TEdit) then with (obj as TEdit) do 
        {Editfield: restore text}
        text:= ReadString(INISection,Name,text)
      else if (obj is TMenuItem) then with (obj as TMenuItem) do 
        {Menuitem: restore checked state}
        checked:= ReadBool(INISection,Name,checked)
      else if (obj is TTabbedNoteBook) then with (obj as TTabbedNoteBook) do
        {Notebook: restore open Tab}
        pageIndex:= ReadInteger(INISection,Name,pageIndex)
      else if (obj is TDriveComboBox) then with (obj as TDriveComboBox) do begin    
         {DriveCombo: restore selected drive}
         strBuf := ReadString(INISection,Name,Drive);
         Drive := strBuf[1];
      end else if (obj is TDirectoryListBox) then with (obj as TDirectoryListBox) do
        {DirectoryList: restore current directory}
        Directory:= ReadString(INISection,Name,Directory);
    end
  else
    raise EExtIniFError.create('This object is not supported!');
end;

{ Restores the state of all registered objects from the INI-File}
procedure TExtIniF.RestoreObjectStates;
var
  objNo: integer;
begin
  {iterate through all registered objects}
  for objNo:= 0 to FRegObjects.count - 1 do
    ReStoreObjectState(FRegObjects.objects[objNo],FRegObjects.strings[objNo]);
end;

{ Stores the state of an object to the INI-File}
procedure TExtIniF.StoreObjectState(obj: TObject; INISection: string);
var
  strBuf: string;
begin
  if ( INISection = '' ) then INISection:= FIniSection;
  {the next lines check for the type of object and
   store whatever property we would like to store of that object
   if you make changes here you will need to make changes in
   ReStoreObjectState as well!!!}
  if (obj.classInfo <> nil ) then begin
    if (obj is TCheckBox) then with (obj as TCheckBox) do
      {Checkboxes: store checked state}
      writeBool(INISection,Name,checked)
    else if (obj is TEdit) then with (obj as TEdit) do
      {Editfield: store text}
      writeString(INISection,Name,text)
    else if (obj is TMenuItem) then with (obj as TMenuItem) do
      {Menuitem: restore checked state}
      writeBool(INISection,Name,checked)
    else if (obj is TTabbedNoteBook) then with (obj as TTabbedNoteBook) do
      {Notebook: restore open Tab}
      writeInteger(INISection,Name,pageIndex)
    else if (obj is TDriveComboBox) then with (obj as TDriveComboBox) do
      {DriveCombo: restore selected drive}
      writeString(INISection,Name,Drive)
    else if (obj is TDirectoryListBox) then with (obj as TDirectoryListBox) do
      {DirectoryList: restore current directory}
      writeString(INISection,Name,Directory)
    else
      raise EExtIniFError.create('This object is not supported!');
  end;
end;

{ Stores the state of all registered objects to the INI-File}
procedure TExtIniF.StoreObjectStates;
var
  objNo: integer;
begin
  for objNo:= 0 to FRegObjects.count - 1 do
    StoreObjectState(FRegObjects.objects[objNo],FRegObjects.strings[objNo]);
end;

end.                                                                                                                                                                                                                                                                                                                                                                                                                          {To enable list view column sorting simply override the list view column click
 procedure with this method.  This algorithm will invert sort a column if you
 click on it twice.  If you don't like that, then remove references to lastix}

procedure TForm1.ListView1ColumnClick(sender: tobject; column: tlistcolumn);
const 
  asc : boolean = true;
  lastix : integer = -1;

  function customsortproc(item1, item2: tlistitem; paramsort: integer): integer; stdcall;
  var 
    sr1,sr2 : string;
  begin
    // get the strings to compare (depending on lastix)
    if lastix = 0 then begin
      sr1 := item1.caption;
      sr2 := item2.caption
    end else begin
      sr1:= item1.subitems[lastix-1];
      sr2:= item2.subitems[lastix-1];
    end;
    // now compare the strings
    result := lstrcmp(pchar(sr1),pchar(sr2));
    // if we are not ascending, invert the result
    if not asc then 
      result := -result;
  end;

begin
  // compare the column-index with the last one, if not the same,    Topic"   Listview Sorting via Column Clicks   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                set asc 
  // (ascending sorting) to true
  if column.index <> lastix then 
    asc := true
  else 
    asc := not asc;
  lastix := column.index;

 // now sort the items
 listview1.customsort(@customsortproc,0);
end;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            uses printers;

procedure TForm1.PrintIt(Sender: TObject);
var
  PrintBuf: TextFile;
begin
  AssignPrn(PrintBuf);
  Rewrite(PrintBuf);
  try
    for i := 0 to Memo1.Lines.Count-1 do
      WriteLn(PrintBuf, Memo1.Lines[i]);
  finally
    CloseFile(PrintBuf);
  end;
end;

{Found on the Borland Forums}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       Topic   Memo Printing   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     {Try locking the window update before changing the color}

LockWindowUpdate(RichEdit1.Handle);
try
  {Do Color stuff}
finally
  LockWindowUpdate(0);
end;

{code by Rick Seiden}

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      Topic   RichEdit Flicker Reduction   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        {Add the following method to your component}
procedure CMDesignHitTest(var Message: TCMDesignHitTest); message CM_DESIGNHITTEST;

{The implemenation should look like this : }
procedure TMyButton.CMDesignHitTest(var Message: TCMDesignHitTest);
begin
  Message.Result := 0;
end;

{Code written by Dan}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             Topic,   Making a Component Responsive at Design-Time   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      {A Page Control component that uses accelerator keys}

unit accel;

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms,
  Dialogs, ComCtrls;

type
  TAccelPageCtrl = class(TPageControl)
  private
    { Private declarations }
    procedure CMDialogChar(var Msg: TCMDialogChar); message CM_DIALOGCHAR;
  end;

procedure Register;

implementation

procedure TAccelPageCtrl.CMDialogChar(var Msg: TCMDialogChar);
var
  I: Integer;
begin
  inherited; //call the inherited message handler.
  //Now with our own component, start at Page 1 (Item 0) and work to the end.
  for I := 0 to PageCount - 1 do begin
    //If accelerator key in caption matches page then change the page and
    // break out of the loop.  
    if (IsAccel(Msg.CharCode, Pages[I].Caption) AND CanChange(I)) then begin
      Msg.Result := 1; //you can set this to anything, but by convention it's 1
      ActivePage := Pages[I];
      Change;
      Break;
    end;
  end;
  
end;

procedur   Topic   Page Control Accellerator Keys   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    e Register;
begin
  RegisterComponents('Samples', [TAccelPageCtrl]);
end;

end.

{As you can see from the above. all that's required to add accelerator
key response is a simple message handler procedure. The message we're
interested in is CM_DialogChar, a Delphi custom message type
encapsulated by TCMDialogChar, which is a wrapper type for the
Windows WM_SYSCHAR message. WM_SYSCHAR is the Windows message that is
used to trap accelerator keys; you can find a good discussion of it in
the online help. The most important thing to note is what happens when
the TAccelPageCtrl component detects that a CM_DialogChar message has
fired. 

Take a look at the CMDialogChar procedure, and note that all that's
going in the code is a simple for loop that starts at the first page
of the descendant object and goes to the last page, unless the key that
was pressed happened to be an accelerator key. We can easily determine
if a key is an accelerator key with the IsAccel function, which takes
the key code pressed and a string (we passed the Caption property of
the current TabSheet). IsAccel searches through the string and looks
for a matching accelerator key. If it finds one, it returns True. If
so, we set the message result value and change the page of
TAccelPageCtrl to the page where the accelerator was found by setting
the ActivePage property and calling the inherited Change procedure from
TPageControl. 

I haven't used TPageControl since I created this component because of
how easy TAccelPageCtrl makes switching from TabSheet to TabSheet.
It's far easier to do a Alt-<key> combination than use the mouse when
you're at the keyboard. Play around with this and you'll be convinced
not to use the standard VCL TPageControl.}                                                                                                                                                                                                                                                                                            {I have done it. The main code fragments you are interested in are below.
The thing that makes the difference is intercepting the WM_MOVE message,
and calling the function InvalidateFrame. By the way - I copied
InvalidateFrame from the CONTROLS.PAS unit - unfortunately, it is
private to TWinControl :-(  }

------------------------------------------------------------------------

... other stuff snipped ...

type
  TBackgroundStyle = (bsOpaque, bsTransparent);

  TCustomButtonPanel = class(TScrollBox)
    private
      FCanvas: TCanvas;  { Need a Canvas }
    protected
      procedure WMSize(var Message: TWMSize); message WM_SIZE;
      procedure WMPaint(var Message: TWMPaint); message WM_PAINT;
      procedure WMMove(var Message: TWMMove); message WM_MOVE;
      procedure CreateParams(var Params: TCreateParams); override;
      procedure PaintWindow(DC: HDC); override;
      procedure Paint; virtual;
      procedure InvalidateFrame;
      property BackgroundStyle:  TBackgroundStyle
     Topic   Creating Transparent Controls   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               read FBackgroundStyle
            write SetBackgroundStyle
            default bsOpaque;
      ... other stuff snipped ...
    public
      constructor Create(AOwner: TComponent); override;
      property Canvas: TCanvas read FCanvas;
      ... other stuff snipped ...
  end;

... other code and stuff snipped ...

implementation

constructor TCustomButtonPanel.Create(AOwner: TComponent);
begin
  FBackgroundStyle := bsOpaque;
  inherited Create(AOwner);
  ControlStyle := [csAcceptsControls, csCaptureMouse, csClickEvents,
                   csSetCaption, csOpaque, csDoubleClicks];
  FCanvas := TControlCanvas.Create;
  TControlCanvas(FCanvas).Control := Self;
end;

procedure TCustomButtonPanel.SetBackgroundStyle(Value:TBackgroundStyle);
begin
  { BackgroundStyle Set Property Handler }
  if Value <> FBackgroundStyle then begin
    FBackgroundStyle := Value;
    RecreateWnd;
  end;
end;

procedure TCustomButtonPanel.CreateParams(var Params: TCreateParams);
begin
  inherited CreateParams(Params);
  with Params do begin
    if FBackgroundStyle = bsOpaque then
      ExStyle := ExStyle and not Ws_Ex_Transparent
    else
      ExStyle := ExStyle or Ws_Ex_Transparent;
  end;
end;

procedure TCustomButtonPanel.PaintWindow(DC: HDC);
begin
  { Setup the canvas and call the Paint routine }
  FCanvas.Handle := DC;
  try
    Paint;
  finally
    FCanvas.Handle := 0;
  end;
end;

procedure TCustomButtonPanel.Paint;
var
  theRect: TRect;
begin
  with canvas do
    brush.Color := Self.Color;
    theRect := GetClientRect;
    if FBackgroundStyle = bsOpaque then
      FillRect(theRect);
  ... other code and stuff snipped ...
  end;
end;

procedure TCustomButtonPanel.InvalidateFrame;
var
  R: TRect;
begin
  { Handle invalidation after move in designer }
  R := BoundsRect;
  InflateRect(R, 1, 1);
  InvalidateRect(Parent.Handle, @R, True);
end;

procedure TCustomButtonPanel.WMMove(var Message: TWMMove);
begin
  if (csDesigning in ComponentState) then
    InvalidateFrame;
  inherited;
end;

... other code and stuff snipped ...

{Found in the Borland Forums}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             {Menus, as implemented by the Windows API, do not take focus the same way as 
controls do. When a menu is activated Windows takes over and starts an 
internal message loop that does not return control to your app until the 
menu is closed one way or another.

To remote-control a menu you fake keystrokes: F10 to activate the main 
menu, then the shortcut letter of the main menu item to select and open, 
then key downs to move the hilight bar to the menu item you want to select. 

Example to select the "Copy" entry if the standard "Edit" menu:}

procedure PostVKey(hWindow: HWND; key: Word);
begin
  if iswindow(hWindow) then begin
    PostMessage(hWindow, WM_KEYDOWN, key, MakeLong(0, MapVirtualKey(key, 0)));
    PostMessage(hWindow, WM_KEYUP, key, MakeLong(1, MapVirtualKey(key, 0) or $C000));
  end;
end;

procedure TForm1.FakeIt(Sender: TObject);
var
  i: Integer;
begin
  PostVKey(Handle, VK_F10);
  PostMessage(Handle, WM_CHAR, Ord('e'), 0);
  for i:= 1 To 3 Do
    PostVKey(Handle, VK_D   Topic$   Opening - Manipulating Menus in Code   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 �   F    Status Bar - Components in    �   �   Status Bar - Components in   �   <   $ Checking for Intersecting Components    �   :  $ Checking for Intersecting Components   �   F    Memo Auto-Scrolling    �   �    Memo Auto-Scrolling   �   5    Edit Control Undo    �   ;    Edit Control Undo   �   3    Listbox Horizontal Scroll Bar    �   �    Listbox Horizontal Scroll Bar   �   ?                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             OWN);
end;

{Code written by Peter Below (TeamB)}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
// Another alternative would be to use this TStatusbar decendant which
// accepts any control.

unit NewStatusBar;

interface

uses
  Classes, Windows, Controls, Comctrls;

type
  TACStatusBar = class(TStatusBar)
  private
    { Private declarations }
  protected
    { Protected declarations }
  public
    { Public declarations }
   constructor Create(aOwner: TComponent); override;
  published
    { Published declarations }
  end;

procedure Register;

implementation

constructor TACStatusBar.Create(aOwner: TComponent);
begin
  inherited Create(aOwner);
  ControlStyle := ControlStyle + [csAcceptsControls];
end;  

procedure Register;
begin
  RegisterComponents('Samples', [TACStatusBar]);
end;

end.
                                                                                                                                                                                                                                                                                      Topic   Status Bar - Components in   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        {I think this should do the trick, it uses screen coordinates to avoid 
the problem (checking controls with distinct parents) you noted.}

function DoTheyIntersect(a,b :TControl) : boolean;
var
  ra, rb, dud : TRect;
begin
  ra.TopLeft := a.Parent.ClientToScreen(a.BoundsRect.TopLeft);
  ra.BottomRight := a.Parent.ClientToScreen(a.BoundsRect.BottomRight);
  rb.TopLeft := b.Parent.ClientToScreen(b.BoundsRect.TopLeft);
  rb.BottomRight := b.Parent.ClientToScreen(b.BoundsRect.BottomRight);
  
  Result := IntersectRect(dud, ra,rb);
end;

{Chris Hill}

                                                                                                                                                                                                                                                                                                                                                                                                                                                                         Topic$   Checking for Intersecting Components   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              {This line of code scrolls the caret into view}

 memo.Perform(EM_SCROLLCARET, 0, 0 );
 
{Code written by Peter Below (TeamB)}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               Topic   Memo Auto-Scrolling   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               if getfocus<>0 then SendMessage(GetFocus, EM_UNDO, 0, 0);
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Topic   Edit Control Undo   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 {To add a horizontal scroll bar to a list box use the following line of code}

sendmessage(ListBox.Handle, LB_SetHorizontalExtent, PixelWidth , 0);
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Topic   Listbox Horizontal Scroll Bar   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      Simulating a Tab    �   &    Simulating a Tab   �   2    Stopping Flicker Problems    �   ,   Stopping Flicker Problems   �   ;   ' Restoring Focus to the Previous Control    �   �  ' Restoring Focus to the Previous Control   �   I   " Putting a Bitmap in the Background    �   �  " Putting a Bitmap in the Background   �   D    Trapping a Minimize Action    �   �   Trapping a Minimize Action   �   <    Trapping the Tab Key    �   �   Trapping the Tab Key   �   6   ' Intercepting Component to Form Messages    �   �  ' Intercepting Component to Form Messages   �   I    Restricting Window Sizes    �   7   Restricting Window Sizes   �   :   ! Moving Forms Without the Titlebar    �   e  ! Moving Forms Without the Titlebar   �   C    Making a Form Non-Moveable    �   #   Making a Form Non-Moveable   �   <    Making a "Transparent" Form    �   �   Making a "Transparent" Form   �   =   $ Determining a Form's ScrollBar Width    �   /   $ Determining a Form's ScrollBar Width   �   F    Topic   Form Level Stuff                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   SelectNext(ActiveControl,True,True);
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             Topic   Simulating a Tab   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  {This technique is useful for eliminating flicker problems caused by full
 erases when the redraw will completely cover the 'damaged' area, thus 
 making the full erase unnecessary
 
 Essentially, we intercept the erase command then say we handled it with
 out doing anything.  This can be refined to only 'handle' certain portions
 of the screen on a use by use basis}

Procedure TForm1.WMEraseBkGnd(Var Msg:TMessage); 
Begin
  Msg.Result:= 1; { Do nothing, but mark the message as being handled}
End;

{This technique was posted by Andrew}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       Topic   Stopping Flicker Problems   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         {This code shows how to return focus to a control after some event has
changed it.  If you want to click on some button, then return focus to
the previously selected control, you can probably use a speed button
instead of a button, since the speed button won't steal focus.  If this
doesn't work, try the following code.

This involves using the Screen.OnActiveControlChange event, as 
illustrated in the following code example.}

unit Unit1;

interface

uses
  SysUtils, WinTypes, WinProcs, Messages, Classes, Graphics, Controls,
  Forms, Dialogs, StdCtrls;

type
  TForm1 = class(TForm)
    ListBox1: TListBox;
    ButtonPrint: TButton;
    Memo1: TMemo;
    procedure FormCreate(Sender: TObject);
    procedure ButtonPrintClick(Sender: TObject);
  private
    FPreviousControl : TWinControl;
    FCurrentControl : TWinControl;
  public
    procedure FocusPreviousControl;
    procedure ActiveControlChangeHandler(Sender: TObject);
    property PreviousControl: TWinControl read FPreviousCont   Topic'   Restoring Focus to the Previous Control   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           rol;
  end;

var
  Form1: TForm1;

implementation

{$R *.DFM}

procedure TForm1.FocusPreviousControl;
begin
  if (FPreviousControl <> nil) then 
    FPreviousControl.SetFocus;
end;

procedure TForm1.ActiveControlChangeHandler(Sender: TObject);
begin
  { This event fires AFTER the active control is changed }
  if (ActiveControl is TWinControl) then begin
    FPreviousControl := FCurrentControl;
    FCurrentControl := ActiveControl;
  end;
end;

procedure TForm1.FormCreate(Sender: TObject);
begin
  Screen.OnActiveControlChange := ActiveControlChangeHandler;
end;

procedure TForm1.ButtonPrintClick(Sender: TObject);
begin
  { Print stuff here }
  FocusPreviousControl;
end;

end.

{coded by Rick Rogers (TeamB)}                                                                                                                                                                                                                                                                               {To place a bitmap in the background of a form

  1) Place the bitmap in a resource file. (Your forms, the applications, 
     or your own).  Make sure that however you do it the resource file
     is linked into your form.
  2) Create a TBitmap variable in the private section of the form.     
  3) Override the OnCreate, OnDestroy and OnPaint methods of the form}
  
procedure TForm1.FormCreate(Sender: TObject);
begin
  BackgroundBitmap := TBitmap.Create;
  BackgroundBitmap.LoadFromResourceName(hInstance,'MYBITMAP');
end;
 
procedure TForm1.FormDestroy(Sender: TObject);
begin
  BackgroundBitmap.Free;
end;
 
procedure TForm1.FormPaint(Sender: TObject);
begin
  Canvas.Draw(0,0,BackgroundBitmap);
end;                                                                                                                                                                                                                                                                                                          Topic"   Putting a Bitmap in the Background   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                {add a handler for the message WM_SYSCOMMAND to your window. Look for 
the case (msg.CmdType and $FFF0) = SC_MINIMIZE and do not call inherited 
for this case, instead return msg.result := 0. Call inherited for all other 
cases.}

procedure WMSYSCOMMAND(msg : TMessage); message WM_SYSCOMMAND;
begin
  if msg.CmdType and $FFF0 = SC_MINIMIZE) then
    msg.result := 0
  else
    inherited;
end;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               Topic   Trapping a Minimize Action   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        {This technique may be helpful to you if you want the tab to 
 move columns with in a stringgrid, etc. instead of performing
 its default action}
 
unit TrappingTabs;

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  StdCtrls, Grids;

type
  TForm1 = class(TForm)
    Edit1: TEdit;
    Button1: TButton;
    procedure FormKeyDown(Sender: TObject; var Key: Word;
      Shift: TShiftState);
  private
    { Private declarations }
    procedure WMGetDlgCode(var msgIn: TWMGetDlgCode); message WM_GETDLGCODE;
  public
    { Public declarations }
  end;

var
  Form1: TForm1;

implementation

{$R *.DFM}

procedure TForm1.WMGetDlgCode;
begin
   inherited;
   msgIn.Result := msgIn.Result or DLGC_WANTTAB;
end;

procedure TForm1.FormKeyDown(Sender: TObject; var Key: Word;
  Shift: TShiftState);
begin
  if Key = VK_TAB then
     ShowMessage('Tab');
end;

end.
                                                                                 Topic   Trapping the Tab Key   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              unit FTSubCls;
{How to intercept messages that a form gets from one of it's components} 
interface

uses
SysUtils, WinTypes, WinProcs, Messages, Classes, Controls, Forms;

type

  TFTSubclassWnd = class(TComponent)
  private
    FNewWndProcPtr : TFarProc;
    FOldWndProcPtr : TFarProc;
    FWindowHandle : HWnd;
  protected
    { Virtual methods for descendants }
    procedure NewWndProc(var Message: TMessage); virtual; abstract;
    procedure AssignHandle; virtual;
    { Component methods }
    procedure ReplaceWndProc;
    procedure RestoreWndProc;
    procedure CallOldWndProc(var Message: TMessage);
    { Protected properties }
    property NewWndProcPtr: TFarProc read FNewWndProcPtr;
    property OldWndProcPtr: TFarProc read FOldWndProcPtr;
    property WindowHandle: HWnd read FWindowHandle;
  public
    { Construction/destruction }
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;
  end;

implementation

constructor TFTSubclassWnd.Cre   Topic'   Intercepting Component to Form Messages   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           ate(AOwner: TComponent);
begin
  inherited Create(AOwner);
  if not (AOwner is TForm) then 
    raise Exception.Create('Owner must be form');
  AssignHandle;
  ReplaceWndProc;
end;

destructor TFTSubclassWnd.Destroy;
begin
  RestoreWndProc;
  inherited Destroy;
end;

procedure TFTSubclassWnd.CallOldWndProc(var Message: TMessage);
begin
  with Message do
    Result := CallWindowProc(FOldWndProcPtr, FWindowHandle, Msg, wParam, lParam);
end;

procedure TFTSubclassWnd.AssignHandle;
begin
  with (Owner as TForm) do begin
    { Ensure the window handle has been allocated }
    HandleNeeded;
    { Assign window handle (with special processing for MDI parent forms }
    if (FormStyle = fsMDIForm) then 
      FWindowHandle := ClientHandle
    else 
      FWindowHandle := Handle;
  end;
end;

procedure TFTSubclassWnd.ReplaceWndProc;
begin
  { Save pointer to old WndProc }
  FOldWndProcPtr := Pointer(GetWindowLong(FWindowHandle, GWL_WNDPROC));
  { Create pointer to NewWndProc }
  FNewWndProcPtr := MakeObjectInstance(NewWndProc);
  if (FNewWndProcPtr = nil) then
    raise EOutOfResources.Create('Cannot allocate WndProc handle');
  { Subclass window by setting GWL_WNDPROC to NewWndProc }
  SetWindowLong(FWindowHandle, GWL_WNDPROC, LongInt(FNewWndProcPtr));
end;

procedure TFTSubclassWnd.RestoreWndProc;
begin
  SetWindowLong(FWindowHandle, GWL_WNDPROC, LongInt(FOldWndProcPtr));
  if FNewWndProcPtr <> nil then 
    FreeObjectInstance(FNewWndProcPtr);
end;

end.

(*
You'll need to descend any components which need to "listen" to the form's
messages from the TSubclassWnd component. In your descendant component, you'll
need to override the NewWndProc procedure, and provide a message handler that
looks for messages of interest. For example, your procedure will look something
like this: 
  Procedure TMaleyComponent.NewWndProc(var Message: TMessage);
  begin
    if (Message.Msg = WM_SIZE) then { Do something };
  end;
*)

{This code was written by Rick Rogers}
      {In your form, override the WM_GETMINMAXINFO method with a call
to the following code}

procedure TForm1.WMGETMINMAXINFO( var message: TMessage );
var
  mStruct: PMinMaxInfo;
begin
  mStruct := PMinMaxInfo(message.lParam);
  mStruct.ptMinTrackSize.x := 480;
  mStruct.ptMinTrackSize.y := 350;
// ptMaxSize.x:=800;      {Width of form when maximized}
// ptMaxSize.y:=600;      {Height of form when maximized}
// ptMaxPosition.x:=0;    {Form.Left when maximized}
// ptMaxPosition.y:=0;    {Form.Top when maximized}
// ptMinTrackSize.x:=400; {min width you can achieve with mouse}
// ptMinTrackSize.y:=200; {min height you can achieve with mouse}
// ptMaxTrackSize.x:=750; {max width you can achieve with mouse}
// ptMaxTrackSize.y:=550; {max height you can achieve with mouse}
  message.Result := 0;
end;
                                                                                                                                                                                                            Topic   Restricting Window Sizes   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          {subject : moving forms (and other twincontrols) without using the caption-bar}

procedure twincontrol.formmousedown(sender: tobject; button: tmousebutton;
                                    shift: tshiftstate; x, y: integer);
const
  sc_dragmove = $f012;
begin
  releasecapture;
  twincontrol(sender).perform(wm_syscommand,sc_dragmove, 0);
end;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              Topic!   Moving Forms Without the Titlebar   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 {Here is how to keep the window from moving:

First, obviously you are going to want to make the borderstyle something
like bsDialog, so that the window cant be resized.
Next, add the following declaration to your form class:
}
procedure PosChanging(var Msg: TWmWindowPosChanging); message WM_WINDOWPOSCHANGING;

// Finally, implement the procedure like:

procedure TMyForm.PosChanging(var Msg: TWmWindowPosChanging);
begin
   Msg.WindowPos.x := Left;
   Msg.WindowPos.y := Top;
   Msg.Result := 0;
end;

{Thats it. Easy as can be.The only problem with this is that you cant move
the form if you want your code to. To get around this, just set up a
Boolean variable called PosLocked, set it to true when you want to lock the
forms position, and to false when you need to move the form (when your
done, remember to set it back to true). Then to implement the proc above,
just make it}

if PosLocked then begin
   Msg.WindowPos.x := Left;
   Msg.WindowPos.y := Top;
   Msg.Result := 0;
end else inh   Topic   Making a Form Non-Moveable   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        erited;

{coded by Ron Frazier}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             {Add a button to a form and try this:}

procedure TForm1.FormCreate(Sender: TObject);
var
  FullRgn, ClientRgn, ButtonRgn: THandle;
  Margin, X, Y: Integer;
begin
  Margin := (Width - ClientWidth) div 2;
  FullRgn := CreateRectRgn(0, 0, Width, Height);
  X := Margin;
  Y := Height - ClientHeight - Margin;
  ClientRgn := CreateRectRgn(X, Y, X + ClientWidth, Y + ClientHeight);
  CombineRgn(FullRgn, FullRgn, ClientRgn, RGN_DIFF);
  X := X + Button1.Left;
  Y := Y + Button1.Top;
  ButtonRgn := CreateRectRgn(X, Y, X + Button1.Width, Y + Button1.Height);
  CombineRgn(FullRgn, FullRgn, ButtonRgn, RGN_OR);
  SetWindowRgn(Handle, FullRgn, True);
end;
                                                                                                                                                                                                                                                                                                                                                                      Topic   Making a "Transparent" Form   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       ScrollSize := GetSystemMetrics(SM_CXVSCROLL);
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Topic$   Determining a Form's ScrollBar Width   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 Creating a TForm Descendant      T   Creating a TForm Descendant     =    Creating a Floating Window    
  y   Creating a Floating Window     <    Adding a Caption Button      Y   Adding a Caption Button     9    Posting Keystrokes from Threads      )   Posting Keystrokes from Threads     A                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           {This explains how to set up a new form descendant which will have editable
 properties via the object inspector}
 

{In Delphi 3.0 there is a new approach which you can use that I will try
to briefly describe.  The idea is to use a custom module to create a new
form class that has new properties etc.  This can be done by registering a
classtype with the IDE by calling RegisterCustomModule (in the expert API).
 With a custom module you can add your own new properties to forms which
will appear in the object inspector.  The mechanism to get this to work is
as follows: 

- Create a unit that declares your new form class descending from TCustomForm
- Create an expert that will generate a new instance of the above class in
  the IDE
- Install the expert in the IDE by including the expert unit below in a new
  package and simply install the package.

Once you have followed these steps you will have a new item on the Form
page of the File|New dialog which will be your new form class.

The new f            	                                                                      =                                 '          *      +  ,      /                                  8      9                                          E      F  G  H  I  J  K              P      Q  R  S  T  U  V  W      Y  Z  [  \  ]  ^  _  `  a  b  c  d  e  f  g  h  i  j  k  l  m  n  o  p  q  r  s  t  u  v  w  x  y  z  {  |  }  ~    �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �  �                Topic   Creating a TForm Descendant   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       orm class unit:}
>>>> Begin unit <<<<<<
unit myForm;

interface

uses Messages, Windows, SysUtils, Classes, Controls, Forms;

type
  TMyForm = class(TCustomForm)
  private
    FNewProp: String;  
  protected
  public
  published
    property NewProp: String read FNewProp write FNewProp;  // ex. of a new form property
    property ActiveControl;
    property Align;
    property AutoScroll;
    property BorderStyle;
    property BorderIcons;
    property Caption stored True;
    property ClientHeight;
    property ClientWidth;
    property Color;
    property DragCursor;
    property DragMode;
    property Enabled;
    property Font;
    property Height stored True;
    property HorzScrollBar;
    property KeyPreview;
    property ParentColor;
    property ParentCtl3D;
    property ParentFont;
    property ParentShowHint;
    property PixelsPerInch;
    property PopupMenu;
    property PrintScale;
    property Scaled;
    property ShowHint;
    property TabStop;
    property VertScrollBar;
    property Width stored True;
    property OnActivate;
    property OnClick;
    property OnClose;
    property OnCloseQuery;
    property OnCreate;
    property OnDblClick;
    property OnDestroy;
    property OnDeactivate;
    property OnDragDrop;
    property OnDragOver;
    property OnHide;
    property OnHelp;
    property OnKeyDown;
    property OnKeyPress;
    property OnKeyUp;
    property OnMouseDown;
    property OnMouseMove;
    property OnMouseUp;
    property OnPaint;
    property OnResize;
    property OnShow;
  end;

implementation

end.
>>>>  end unit <<<<

{Here is the expert necessary to create instances of this class from the IDE:}
>>>> begin expert <<<<
unit newmod;

interface

procedure Register;

implementation

uses Windows, SysUtils, Classes, Controls, Forms, ExptIntf, ToolIntf,
VirtIntf,
  IStreams, DsgnIntf, MyForm;

type
  TMyFormExpert = class(TIExpert)
    function GetName: string; override;
    function GetComment: string; override;
    function GetGlyph: HICON; override;
    function GetStyle: TExpertStyle; override;
    function GetState: TExpertState; override;
    function GetIDString: string; override;
    function GetAuthor: string; override;
    function GetPage: string; override;
    function GetMenuText: string; override;
    procedure Execute; override;
  end;

{ TMyFormExpert }

function TMyFormExpert.GetName: string;
begin
  Result := 'My Form';
end;

function TMyFormExpert.GetComment: string;
begin
  Result := 'Custom form';
end;

function TMyFormExpert.GetGlyph: HICON;
begin
  Result := LoadIcon(HInstance, '');
end;

function TMyFormExpert.GetStyle: TExpertStyle;
begin
  Result := esForm;
end;

function TMyFormExpert.GetState: TExpertState;
begin
  Result := [esEnabled];
end;

function TMyFormExpert.GetIDString: string;
begin
  Result := 'MyForm.Expert';
end;

function TMyFormExpert.GetAuthor: string;
begin
  Result := 'Borland';
end;

function TMyFormExpert.GetPage: string;
begin
  Result := 'Forms';
end;

function TMyFormExpert.GetMenuText: string;
begin
  Result := '';
end;

const
  FormUnitSource =
    'unit %0:s;'#13#10 +
    #13#10 +
    'interface'#13#10 +
    #13#10 +
    'uses Windows, SysUtils, Messages, Classes, Graphics, Controls,'#13#10 +
    '  StdCtrls, ExtCtrls, MyForm;'#13#10 +
    #13#10 +
    'type'#13#10 +
    '  T%1:s = class(TMyForm)'#13#10 +
    '  private'#13#10 +
    '    { Private declarations }'#13#10 +
    '  public'#13#10 +
    '    { Public declarations }'#13#10 +
    '  end;'#13#10 +
    #13#10 +
    'var'#13#10 +
    '  %1:s: T%1:s;'#13#10 +
    #13#10 +
    'implementation'#13#10 +
    #13#10 +
    '{$R *.DFM}'#13#10 +
    #13#10 +
    'end.'#13#10;

  FormDfmSource = 'object %s: T%0:s end';

procedure TMyFormExpert.Execute;
var
  UnitIdent, Filename: string;
  FormName: string;
  CodeStream: TIStream;
  DFMStream: TIStream;
  DFMString, DFMVCLStream: TStream;
begin
  if not ToolServices.GetNewModuleName(UnitIdent, FileName) then Exit;
  FormName := 'MyForm' + Copy(UnitIdent, 5, 255);
  CodeStream :=
TIStreamAdapter.Create(TStringStream.Create(Format(FormUnitSource,
    [UnitIdent, FormName])), True);
  try
    CodeStream.AddRef;
    DFMString := TStringStream.Create(Format(FormDfmSource, [FormName]));
    try
      DFMVCLStream := TMemoryStream.Create;
      try
        ObjectTextToResource(DFMString, DFMVCLStream);
        DFMVCLStream.Position := 0;
      except
        DFMVCLStream.Free;
      end;
      DFMStream := TIStreamAdapter.Create(DFMVCLStream, True);
      try
        DFMStream.AddRef;
        ToolServices.CreateModuleEx(FileName, FormName, 'TMyForm', '',
          CodeStream, DFMStream, [cmAddToProject, cmShowSource, cmShowForm,
            cmUnNamed, cmMarkModified]);
      finally
        DFMStream.Free;
      end;
    finally
      DFMString.Free;
    end;
  finally
    CodeStream.Free;
  end;
end;

procedure Register;
begin
  RegisterCustomModule(TMyForm, TCustomModule);
  RegisterLibraryExpert(TMyFormExpert.Create);
end;

end.
>>>> end expert <<<<

{To get the expert installed you have to add the unit to a package and
install the package.  Once this is complete your new form class will be
available from the File|New dialog.}

{coded by Steven Trefethen}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            If you want the window to stay on top of ALL windows, then
you can just set the form's style to fsStayOnTop.  If you have
a specific window you want to stay on top of, then you need
override the TForm.CreateParams method.  After calling the
inherited method, set the Param.ParentWnd to the handle
of the window you wish to float above.  

{found on the Borland Forums}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          Topic   Creating a Floating Window   Language   N                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        {
Handle message WM_NCPAINT. Call inherited to allow the normal caption to be
drawn, then add your stuff. A good function for drawing buttons (here or
anywhere) is DrawFrameControl.

Handle message WM_NCHITTEST. When the mouse is over your button, don't pass
to inherited. This prevents Windows from thinking your button is a part of
the normal caption and from allowing the window to be dragged at that
point.

Handle the WM_NC mouse mesages (LButtonDown, LButtonUp, etc). Redraw the
button as pushed on mouse down, redraw as not pushed and do your stuff when
mouse up.
}
{by timjd}

                                                                                                                                                                                                                                                                                                                                                                                                                                          Topic   Adding a Caption Button   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           {************************************************************
 * Procedure PostKeyEx
 *
 * Parameters:
 *  hWindow: target window to be send the keystroke
 *  key    : virtual keycode of the key to send. For printable
 *           keys this is simply the ANSI code (Ord(character)).
 *  shift  : state of the modifier keys. This is a set, so you
 *           can set several of these keys (shift, control, alt,
 *           mouse buttons) in tandem. The TShiftState type is 
 *           declared in the Classes Unit.
 *  specialkey: normally this should be False. Set it to True to 
 *           specify a key on the numeric keypad, for example. 
 *           If this parameter is true, bit 24 of the lparam for
 *           the posted WM_KEY* messages will be set. 
 * Description:
 *  This procedure sets up Windows key state array to correctly
 *  reflect the requested pattern of modifier keys and then posts
 *  a WM_KEYDOWN/WM_KEYUP message pair to the target window. Then
 *  Application.ProcessMe   Topic   Posting Keystrokes from Threads   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   ssages is called to process the messages
 *  before the keyboard state is restored.
 * Error Conditions:
 *  May fail due to lack of memory for the two key state buffers.
 *  Will raise an exception in this case.
 * NOTE:
 *  Setting the keyboard state will not work across applications 
 *  running in different memory spaces on Win32 unless AttachThreadInput
 *  is used to connect to the target thread first.
 *Created: 02/21/96 16:39:00 by P. Below
 ************************************************************}
Procedure PostKeyEx( hWindow: HWnd; key: Word; Const shift: TShiftState;
                     specialkey: Boolean );
Type
  TBuffers = Array [0..1] of TKeyboardState;
Var
  pKeyBuffers : ^TBuffers;
  lparam: LongInt;
Begin
  (* check if the target window exists *)
  If IsWindow(hWindow) Then Begin
    (* set local variables to default values *)
    pKeyBuffers := Nil;
    lparam := MakeLong(0, MapVirtualKey(key, 0));

    (* modify lparam if special key requested *)
    If specialkey Then
      lparam := lparam or $1000000;

    (* allocate space for the key state buffers *)
    New(pKeyBuffers);
    try
      (* Fill buffer 1 with current state so we can later restore it.  
         Null out buffer 0 to get a "no key pressed" state. *)
      GetKeyboardState( pKeyBuffers^[1] );
      FillChar(pKeyBuffers^[0], Sizeof(TKeyboardState), 0);

      (* set the requested modifier keys to "down" state in the buffer *)
      If ssShift In shift Then
        pKeyBuffers^[0][VK_SHIFT] := $80;
      If ssAlt In shift Then Begin
        (* Alt needs special treatment since a bit in lparam needs also be set *)
        pKeyBuffers^[0][VK_MENU] := $80;
        lparam := lparam or $20000000;
      End;
      If ssCtrl In shift Then
        pKeyBuffers^[0][VK_CONTROL] := $80;
      If ssLeft In shift Then
        pKeyBuffers^[0][VK_LBUTTON] := $80;
      If ssRight In shift Then
        pKeyBuffers^[0][VK_RBUTTON] := $80;
      If ssMiddle In shift Then
        pKeyBuffers^[0][VK_MBUTTON] := $80;

      (* make out new key state array the active key state map *)
      SetKeyboardState( pKeyBuffers^[0] );

      (* post the key messages *)
      If ssAlt In Shift Then Begin
        PostMessage( hWindow, WM_SYSKEYDOWN, key, lparam);
        PostMessage( hWindow, WM_SYSKEYUP, key, lparam or $C0000000);
      End
      Else Begin
        PostMessage( hWindow, WM_KEYDOWN, key, lparam);
        PostMessage( hWindow, WM_KEYUP, key, lparam or $C0000000);
      End;
      (* process the messages *)
      Application.ProcessMessages;

      (* restore the old key state map *)
      SetKeyboardState( pKeyBuffers^[1] );
    finally
      (* free the memory for the key state buffers *)
      If pKeyBuffers <> Nil Then
        Dispose( pKeyBuffers );
    End; { If }
  End;
End; { PostKeyEx }

{The keyboard state manipulating stuff will only work if you use 
AttachThreadInput on the target thread but it should not be necessary for 
pure Alt-key combinations anyway.}

{written by Peter Below (TeamB)}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Calculating the Week Number      �   Calculating the Week Number     =    Finding Easter in a Given Year      o   Finding Easter in a Given Year     @    Bitwise Operations      b   Bitwise Operations     4                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Topic
   Algorithms                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         {Calculate a week-of-the-year index (0-51) for a given date.
 Week 0 is the week containing the first Sunday of the year.}

function  WeekNum(const TDT:TDateTime) : Word;
var
  Y,M,D:Word;
  dtTmp:TDateTime;
begin
  DecodeDate(TDT,Y,M,D);
  dtTmp:=EnCodeDate(Y,1,1);
  Result:=(Trunc(TDT-dtTmp)+(DayOfWeek(dtTmp)-1)) DIV 7;
  if Result=0 then 
    Result:=51 
  else 
    Result:=Result-1;
end;

{code written by Ernie Deel, EFD Systems}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             Topic   Calculating the Week Number   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       function GetEaster(Year: Integer): TDate;
var
  y, m, d: Word;
  G, I, J, C, H, L: Integer;
  E: TDate;
begin
  G := Year mod 19;
  C := year div 100;
  H := (C - C div 4 - (8*C+13) div 25 + 19*G + 15) mod 30;
  I := H - (H div 28)*(1 - (H div 28)*(29 div (H + 1))*((21 - G) div 11));
  J := (Year + Year div 4 + I + 2 - C + C div 4) mod 7;
  L := I - J;
  m := 3 + (L + 40) div 44;
  d := L + 28 - 31*(m div 4);
  y := Year;
  // E is the date of the full moon
  E := EncodeDate(y, m, d);
  // Find next sunday
  while DayOfWeek(E) > 1 do
    E := E + 1;
  Result := E;
end;

{ From Yorai Aminov }
                                                                                                                                                                                                                                                                                                                                                                                                                    Topic   Finding Easter in a Given Year   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    unit Bitwise;

interface
  function IsBitSet(const val: longint; const TheBit: byte): boolean;
  function BitOn(const val: longint; const TheBit: byte): LongInt;
  function BitOff(const val: longint; const TheBit: byte): LongInt;
  function BitToggle(const val: longint; const TheBit: byte): LongInt;

implementation

function IsBitSet(const val: longint; const TheBit: byte): boolean;
begin
  result := (val and (1 shl TheBit)) <> 0;
end;

function BitOn(const val: longint; const TheBit: byte): LongInt;
begin
  result := val or (1 shl TheBit);
end;

function BitOff(const val: longint; const TheBit: byte): LongInt;
begin
  result := val and ((1 shl TheBit) xor $FFFFFFFF);
end;

function BitToggle(const val: longint; const TheBit: byte): LongInt;
begin
  result := val xor (1 shl TheBit);
end;

end.

{code found in a Borland TI}                                                                                                                                                                 Topic   Bitwise Operations   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                # Getting Program Version Information      C  # Getting Program Version Information     E    Disabling System Keys    !     Disabling System Keys   "  7    Using the RunOnce Registry Key    #  m   Using the RunOnce Registry Key   $  @   # Trapping Application Global HotKeys    %    # Trapping Application Global HotKeys   &  E   $ Sending Keystrokes to an Application    (  ]  $ Sending Keystrokes to an Application   )  F   / Preventing Multiple Instances of an Application    -    / Preventing Multiple Instances of an Application   .  Q    Remove App from Task Bar    0  M   Remove App from Task Bar   1  :    Extracting an Icon from a File    2     Extracting an Icon from a File   3  @   $ Extracting Version Build Information    4  
  $ Extracting Version Build Information   5  F   ) Execute a Program and Wait for Completion    6  �  ) Execute a Program and Wait for Completion   7  K    Displaying a Help File    :  �   Displaying a Help File   ;  8       Topic   Application Level Code                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             {This sample project demonstrates how to get at the version info
in executables.

 In a new project, drop a TMemo and a TButton on the form (make
 sure they're called Memo1 and Button1). Double click on Button1.
 Now you can go ahead and directly replace the code for the
 Button1Click procedure (see below).

--------------------------
The Button1Click procedure
--------------------------
}
procedure TForm1.Button1Click(Sender: TObject);
const
  InfoNum = 10;
  InfoStr : array [1..InfoNum] of String =
    ('CompanyName', 'FileDescription', 'FileVersion', 'InternalName',
     'LegalCopyright', 'LegalTradeMarks', 'OriginalFilename',
     'ProductName', 'ProductVersion', 'Comments');
var
  S         : String;
  n, Len, i : Integer;
  Buf       : PChar;
  Value     : PChar;
begin
  S := Application.ExeName;
  n := GetFileVersionInfoSize(PChar(S),n);
  if n > 0 then begin
    Buf := AllocMem(n);
    Memo1.Lines.Add('FileVersionInfoSize='+IntToStr(n));
    GetFileVersionInfo(PChar(S),0,   Topic#   Getting Program Version Information   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               n,Buf);
    for i:=1 to InfoNum do
      if VerQueryValue(Buf,PChar('StringFileInfo\040904E4\'+
                                 InfoStr[i]),Pointer(Value),Len) then
        Memo1.Lines.Add(InfoStr[i]+'='+Value);
    FreeMem(Buf,n);
  end else
    Memo1.Lines.Add('No FileVersionInfo found');
end;

{Borland TI}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             procedure TurnSysKeysOff;
var
  Dummy : LongInt;
begin
  SystemParamersInfo(97, Word (True), @Dummy, 0);
end;

procedure TurnSysKeysOn;
var
  Dummy : LongInt;
begin
  SystemParamersInfo(97, Word (False), @Dummy, 0);
end;

{Tips from Meikel Weber}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               Topic   Disabling System Keys   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             {Under Win32, unless you are running from a removable drive, you cannot delete
a running executable. You can have Windows delete the executable the next time
Windows is run by adding an entry to the RunOnce key in the Window registry under:

 HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\RunOnce

You can name the key anything you like, and specify a command line to
another executable or to a dos command made passed to command.com.}

uses
  Registry;

procedure TForm1.Button1Click(Sender: TObject);
var
  reg: TRegistry;
begin
  reg := TRegistry.Create;
  reg.RootKey := HKEY_LOCAL_MACHINE;
  reg.LazyWrite := false;
  reg.OpenKey('Software\Microsoft\Windows\CurrentVersion\RunOnce',
              false);
  reg.WriteString('Delete Me!','command.com /c del FILENAME.EXT');
  reg.CloseKey;
  reg.free;
end;

{coded by Joe C. Hecht}
                                                                                                                                                      Topic   Using the RunOnce Registry Key   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    {To capture the escape key across an entire application, try}

 unit EscapeKey;

 interface

 uses
   SysUtils, WinTypes, WinProcs, Messages, Classes, Graphics, Controls,
   Forms, Dialogs, StdCtrls;

 type
   TFrmEscapeKey = class(TForm)
     Edit1: TEdit;
     Edit2: TEdit;
     Edit3: TEdit;
     Edit4: TEdit;
     Memo1: TMemo;
     Button1: TButton;
     procedure FormCreate(Sender: TObject);
   private { Private-Deklarationen }
     procedure AppMessage(var Msg: TMsg; var Handled: Boolean);
   public { Public-Deklarationen }
   end;

 var
   FrmEscapeKey: TFrmEscapeKey;

 implementation

 {$R *.DFM}

 procedure TFrmEscapeKey.AppMessage(var Msg: TMsg; var Handled: Boolean);
 begin
   if Msg.Message = WM_KEYDOWN then
     if Msg.wParam = VK_ESCAPE then begin
       Application.Terminate;
       Handled := true
     end;
 end;

 procedure TFrmEscapeKey.FormCreate(Sender: TObject);
 begin
   Application.OnMessage := AppMessage;
 end;

 end.

{Ralph (TeamB)}

   Topic#   Trapping Application Global HotKeys   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              {************************************************************
 * Procedure PostKeyEx
 *
 * Parameters:
 *  hWindow: target window to be sent the keystroke
 *  key    : virtual keycode of the key to send. For printable
 *           keys this is simply the ANSI code (Ord(character)).
 *  shift  : state of the modifier keys. This is a set, so you
 *           can set several of these keys (shift, control, alt,
 *           mouse buttons) in tandem. The TShiftState type is 
 *           declared in the Classes Unit.
 *  specialkey: normally this should be False. Set it to True to 
 *           specify a key on the numeric keypad, for example. 
 *           If this parameter is true, bit 24 of the lparam for
 *           the posted WM_KEY* messages will be set. 
 * Description:
 *  This procedure sets up Windows key state array to correctly
 *  reflect the requested pattern of modifier keys and then posts
 *  a WM_KEYDOWN/WM_KEYUP message pair to the target window. Then
 *  Application.ProcessMe   Topic$   Sending Keystrokes to an Application   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              ssages is called to process the messages
 *  before the keyboard state is restored.
 * Error Conditions:
 *  May fail due to lack of memory for the two key state buffers.
 *  Will raise an exception in this case.
 * NOTE:
 *  Setting the keyboard state will not work across applications 
 *  running in different memory spaces on Windows NT! The routine
 *  has not been tested on Windows 95!
 *
 *Created: 02/21/96 16:39:00 by P. Below
 ************************************************************}
Procedure PostKeyEx( hWindow: HWnd; key: Word; Const shift: TShiftState;
                     specialkey: Boolean );
Type
  TBuffers = Array [0..1] of TKeyboardState;
Var
  pKeyBuffers : ^TBuffers;
  lparam: LongInt;
Begin
  (* check if the target window exists *)
  If IsWindow(hWindow) Then Begin
    (* set local variables to default values *)
    pKeyBuffers := Nil;
    lparam := MakeLong(0, VKKeyScan(key));

    (* modify lparam if special key requested *)
    If specialkey Then
      lparam := lparam or $1000000;

    (* allocate space for the key state buffers *)
    New(pKeyBuffers);
    try
      (* Fill buffer 1 with current state so we can later restore it.  
         Null out buffer 0 to get a "no key pressed" state. *)
      GetKeyboardState( pKeyBuffers^[1] );
      FillChar(pKeyBuffers^[0], Sizeof(TKeyboardState), 0);

      (* set the requested modifier keys to "down" state in the buffer *)
      If ssShift In shift Then
        pKeyBuffers^[0][VK_SHIFT] := $80;
      If ssAlt In shift Then Begin
        (* Alt needs special treatment since a bit in lparam needs also be set *)
        pKeyBuffers^[0][VK_MENU] := $80;
        lparam := lparam or $20000000;
      End;
      If ssCtrl In shift Then
        pKeyBuffers^[0][VK_CONTROL] := $80;
      If ssLeft In shift Then
        pKeyBuffers^[0][VK_LBUTTON] := $80;
      If ssRight In shift Then
        pKeyBuffers^[0][VK_RBUTTON] := $80;
      If ssMiddle In shift Then
        pKeyBuffers^[0][VK_MBUTTON] := $80;

      (* make out new key state array the active key state map *)
      SetKeyboardState( pKeyBuffers^[0] );

      (* post the key messages *)
      If ssAlt In Shift Then Begin
        PostMessage( hWindow, WM_SYSKEYDOWN, key, lparam);
        PostMessage( hWindow, WM_SYSKEYUP, key, lparam or $C0000000);
      End
      Else Begin
        PostMessage( hWindow, WM_KEYDOWN, key, lparam);
        PostMessage( hWindow, WM_KEYUP, key, lparam or $C0000000);
      End;
      (* process the messages *)
      Application.ProcessMessages;

      (* restore the old key state map *)
      SetKeyboardState( pKeyBuffers^[1] );
    finally
      (* free the memory for the key state buffers *)
      If pKeyBuffers <> Nil Then
        Dispose( pKeyBuffers );
    End; { If }
  End;
End; { PostKeyEx }

{coded by Peter Below (TeamB) }
                                                                                                                                                                   {there are several approaches to preventing multiple instances of an
 application.  The one I favour is the following: make sure your main
 form has a unique name that is unlikely to crop up an any other
 application. Then on your projects DPR file, before any of your
 forms is created, you do a }

  If FindWindow( Pchar(TMyMainform.Classname), Nil ) Then
    Exit;
    
{This prevents the second instance from ever coming up. If you have the 
additional goal of activating the first instance, use a slightly different 
approach:}

  hPrevWindow := FindWindow( Pchar(TMyMainform.Classname), Nil );
  If hPrevWindow <> 0 Then Begin
    PostMessage( hPrevWindow, UM_ACTIVATEFIRSTINSTANCE, 0, 0 );
    Exit;
  End;  
  
{UM_ACTIVATEFIRSTINSTANCE is a constant (WM_USER + something) you define in 
the interface section of your main forms unit. The main form also gets a 
handler for this message:}

  private
    Procedure UMActivateFirstInstance( Var msg: TMessage );
      message UM_ACTIVATEFIRSTI   Topic/   Preventing Multiple Instances of an Application   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   NSTANCE;
      
{implemented as}

 Procedure TMyMainform.UMActivateFirstInstance( Var msg: TMessage );
 Begin
   If IsIconic(Application.Handle) Then
     Application.restore;
   BringTofront;  
 End;  
 
{That's all there is to it. }

{Peter Below (TeamB)}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                procedure TForm1.FormCreate(Sender: TObject);
begin
  ShowWindow(Application.Handle, SW_HIDE);
  SetWindowLong(Application.Handle, GWL_EXSTYLE,
                etWindowLong(Application.Handle, GWL_EXSTYLE) or
                WS_EX_TOOLWINDOW );
  ShowWindow( Application.Handle, SW_SHOW );
end;
{submitted by Deepak Shenoy}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      Topic   Remove App from Task Bar   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          uses shellapi ; // don't forget

function getnumoficons(a:tfilename):integer;
begin
  // get the count of icons in the in "a" specified file
  result := extracticon(hinstance,pchar(a),-1);
end;

function getfileicon(a:tfilename;index:integer):integer;
begin
  // get the index'st icon from the given filename
  result := -1;
  if getnumoficons(a) > index then // check range
   result := extracticon(hinstance,pchar(a),index);
end;

// extracts a file-icon to a tbitmap (allows stretching, a ticon cannot be drawn stretched)
procedure getbitmapfromfileicon(a:tfilename;index:integer;var bmp:tbitmap);
var icon : ticon;
 ix   : integer;
begin
     ix:= getfileicon(a,index);
  if ix > -1 then try
  icon := ticon.create;
  icon.handle := ix;
  bmp.width := icon.width;
  bmp.height := icon.height;
  bmp.canvas.draw(0,0,icon);
  finally
    icon.free;
  end;
end;
                                                                                                                                    Topic   Extracting an Icon from a File   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    procedure GetBuildInfo(var V1, V2, V3, V4: Word);
var
  VerInfoSize: DWORD;
  VerInfo: Pointer;
  VerValueSize: DWORD;
  VerValue: PVSFixedFileInfo;
  Dummy: DWORD;
begin
  VerInfoSize := GetFileVersionInfoSize(PChar(ParamStr(0)), Dummy);
  if VerInfoSize = 0 then begin
    Dummy := GetLastError;
    ShowMessage(IntToStr(Dummy));
  end; {if}
  GetMem(VerInfo, VerInfoSize);
  GetFileVersionInfo(PChar(ParamStr(0)), 0, VerInfoSize, VerInfo);
  VerQueryValue(VerInfo, '\', Pointer(VerValue), VerValueSize);
  with VerValue^ do begin
    V1 := dwFileVersionMS shr 16;
    V2 := dwFileVersionMS and $FFFF;
    V3 := dwFileVersionLS shr 16;
    V4 := dwFileVersionLS and $FFFF;
  end;
  FreeMem(VerInfo, VerInfoSize);
end;

{coded by Steve Schafer (TeamB)}                                                                                                                                                                                                                                                         Topic$   Extracting Version Build Information   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              {ShellExecute spawns a process asynchronously.  It's as designed that your app
doesn't "wait on" the ShellExecute call for the called app to terminate.

Here's a 32-bit version of "WinExecAndWait":}

function WinExecAndWait32(FileName: string; Visibility: integer): integer;
 { returns -1 if the Exec failed, otherwise returns the process' exit
   code when the process terminates }
var
  zAppName: array[0..512] of char;
  zCurDir: array[0..255] of char;
  WorkDir: string;
  StartupInfo: TStartupInfo;
  ProcessInfo: TProcessInformation;
begin
  StrPCopy(zAppName, FileName);
  GetDir(0, WorkDir);
  StrPCopy(zCurDir, WorkDir);
  FillChar(StartupInfo, Sizeof(StartupInfo), #0);
  StartupInfo.cb := Sizeof(StartupInfo);
  StartupInfo.dwFlags := STARTF_USESHOWWINDOW;
  StartupInfo.wShowWindow := Visibility;
  if not CreateProcess(nil,
    zAppName, { pointer to command line string }
    nil, { pointer to process security attributes }
    nil, { pointer to thread security attributes }
    False   Topic)   Execute a Program and Wait for Completion   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         , { handle inheritance flag }
    CREATE_NEW_CONSOLE or { creation flags }
    NORMAL_PRIORITY_CLASS,
    nil, { pointer to new environment block }
    nil, { pointer to current directory name }
    StartupInfo, { pointer to STARTUPINFO }
    ProcessInfo) then { pointer to PROCESS_INF }
    Result := -1
  else
  begin
    WaitforSingleObject(ProcessInfo.hProcess, INFINITE);
    GetExitCodeProcess(ProcessInfo.hProcess, Result);
    CloseHandle(ProcessInfo.hProcess);
    CloseHandle(ProcessInfo.hThread);
  end;
end;

{OR}

function WinExecAndWait32(FileName: string; Visibility: integer): integer;
 { returns -1 if the Exec failed, otherwise returns the process' exit
   code when the process terminates }
var
  zAppName: array[0..512] of char;
  lpCommandLine: array[0..512] of char;
  zCurDir: array[0..255] of char;
  WorkDir: string;
  StartupInfo: TStartupInfo;
  ProcessInfo: TProcessInformation;
begin
  StrPCopy(zAppName, '');
  StrPCopy(lpCommandLine, FileName);
  GetDir(0, WorkDir);
  StrPCopy(zCurDir, WorkDir);
  FillChar(StartupInfo, Sizeof(StartupInfo), #0);
  StartupInfo.cb := Sizeof(StartupInfo);

  StartupInfo.dwFlags := STARTF_USESHOWWINDOW;
  StartupInfo.wShowWindow := Visibility;
  if not CreateProcess(
    nil, { pointer to command line string }
    lpCommandLine,
    nil, { pointer to process security attributes }
    nil, { pointer to thread security attributes}
    False, { handle inheritance flag }
    CREATE_NEW_CONSOLE or { creation flags }
    NORMAL_PRIORITY_CLASS,
    nil, { pointer to new environment block}
    nil, { pointer to current directory name }
    StartupInfo, { pointer to STARTUPINFO }
    ProcessInfo) then Result := -1 { pointer to PROCESS_INF }
  else begin
    WaitforSingleObject(ProcessInfo.hProcess, INFINITE);
    GetExitCodeProcess(ProcessInfo.hProcess, Result);
    CloseHandle(ProcessInfo.hProcess);
    CloseHandle(ProcessInfo.hThread);
  end;
end;                                                                         {To display a help file in an application }

procedure TMainForm.References1Click(Sender: TObject);
begin
  Application.HelpContext(11);
end;

{or}

procedure TMainForm.Helpwiththisprogram1Click(Sender: TObject);
begin
    Application.HelpCommand(HELP_CONTENTS,0);
end;

{The first calls a specific help context, the second, the contents
page. Please read the online help for full details on these 2 methods.}

{coded by Steve F (Team B)}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          Topic   Displaying a Help File   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            {Save this in Minimal.DPR, open with Delphi, & pick Run button.  I've
only tested this with D2, so if it doesn't work in D3 do let us know.}

    program Minimal; {$APPTYPE CONSOLE}

    procedure Main;
    begin
      WriteLn('Hello, World');
      ReadLn;
    end;
    
    begin 
      Main; 
    end.

{coded by Tony Olekshy}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      Creating a Console Application    <  Z   Creating a Console Application   >  @   + Controlling the Size of Another Application    ?  )  + Controlling the Size of Another Application   @  M    Hooks   A  �    Hooks   B                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          Topic   Creating a Console Application   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    {the following code places notepad directly underneath the running
application:}

var
  hw: HWND;
  wp: TWindowPlacement;
begin
  hw := FindWindow('Notepad', nil);
  if hw <> 0 then
    GetWindowPlacement(Handle, @wp);
  SetWindowPlacement(hw, @wp);
end;

{Xavier Pacheco (TeamB)}

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          Topic+   Controlling the Size of Another Application   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Setting a Windows System Hook    C  7   Setting a Windows System Hook   D  ?    Setting a Window Hook    L  �   Setting a Window Hook   M  7    Setting up a Keyboard Hook    N  D"   Setting up a Keyboard Hook   O  <                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          Topic   Hooks                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              {A DLL is necessary for a system hook.  The sample code below, 
was found somewhere in dejanews.  The first file shows the code
for a simple hook that would be placed in the dll.  Next, a unit
is shown  that provides basic interface to import the dll  The
remaining comments are from the original author}

{This is the core of the system and task hook.  Some notes:             
                                                                         
  1)  You will definitely want to give the file a more descriptive name  
      to avoid possible collisions with other DLL names.                 
  2)  Edit the MouseHookCallBack function to do what you need when a     
      mouse message is received.  If you are hooking something other     
      mouse messages, see the SetWindowsHookEx topic in the help for the 
      proper WH_xxxx constant, and any notes about the particular type   
      of hook.                                                           
  3)  If an application that uses the    Topic   Setting a Windows System Hook   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     DLL crashes while the hook is      
      installed, all manner of wierd things can happen, depending on the 
      sort of thing you are doing in the callback.  The best suggestion  
      is to use a utility that displays loaded DLLs and forcibly unload  
      the DLL.  You could also write a simple app that checks to see if  
      the DLL is loaded, and if so, call FreeModule until it returns 0.  
  4)  If you make changes to the DLL but the changes don't seem to be    
      working, you may have the DLL already loaded in memory.  Remember, 
      loading a DLL that is already in memory just increments a usage    
      count in Windows and uses the already loaded copy.                 
  5)  Remember when you are hooking in at the *system* level, your       
      callback function is being called for everything in the OS.  Try   
      to keep the processing in the callback as tight and fast as you    
      possibly can.                                                      
  6)  Be careful of the uses clause.  If you include stuff like Dialogs, 
      you will end up linking in a lot of the VCL, and have a DLL that   
      comes out compiled to around 250k.  You would probably be better   
      served using WM_USER messages to communicate with the application. 
  7)  I have successfully hooked mouse messages without the use of a     
      DLL, but many of the hooks say they require the callback to be in  
      a DLL, so I am hesitant to include this method.  It certainly      
      makes the build/test cycle *much* easier, but since it is not      
      "sanctioned" by MS, I would stay away from it and discourage it.}

library HookDLL;

uses WinTypes, WinProcs, Messages;
var
  HookCount: integer;
  HookHandle: HHook;

{$IFDEF WIN32}
function MouseHookCallBack(Code: integer; Msg: WPARAM; 
                           MouseHook: LPARAM): LRESULT; stdcall;
{$ELSE}
function MouseHookCallBack(Code: integer; Msg: word; 
                           MouseHook: longint): longint; export;
{$ENDIF}
begin
  { If the value of Code is less than 0, we are not allowed to do anything 
    except pass it on to the next hook procedure immediately. }
  if Code >= 0 then begin
    { This example does nothing except beep when the right mouse button is pressed. }
    if Msg = WM_RBUTTONDOWN then
      MessageBeep(1);

    { If you handled the situation, and don't want Windows to process the 
      message, do *NOT* execute the next line.  Be very sure this is what 
      want, though.  If you don't pass on stuff like WM_MOUSEMOVE, you    
      will NOT like the results you get.                                  }
    Result := CallNextHookEx(HookHandle, Code, Msg, MouseHook);
  end else
    Result := CallNextHookEx(HookHandle, Code, Msg, MouseHook);
end;

{ Call InstallHook to set the hook. }
function InstallHook(SystemHook: boolean; TaskHandle: THandle) : boolean; export;
{This is really silly, but that's the way it goes.  The only way to get the  
 module handle, *not* instance, is from the filename.  The Microsoft example
 just hard-codes the DLL filename.  I think this is a little bit better. }
  function GetModuleHandleFromInstance: THandle;
  var
    s: array[0..512] of char;
  begin
    { Find the DLL filename from the instance value. }
    GetModuleFileName(hInstance, s, sizeof(s)-1);
    { Find the handle from the filename. }
    Result := GetModuleHandle(s);
  end;
begin
 { Technically, this procedure could do nothing but call SetWindowsHookEx(), 
   but it is probably better to be sure about things, and not set the hook    
   more than once.  You definitely don't want your callback being called more 
   than once per message, do you?                                             }
  Result := TRUE;
  if HookCount = 0 then begin
    if SystemHook then
      HookHandle := SetWindowsHookEx(WH_MOUSE, MouseHookCallBack,HInstance, 0)
    else
    { See the Microsoft KnowledgeBase, PSS ID Number: Q92659, for a discussion of 
      the Windows bug that requires GetModuleHandle() to be used.                 }
      HookHandle := SetWindowsHookEx(WH_MOUSE, MouseHookCallBack,
                                     GetModuleHandleFromInstance,TaskHandle);
    if HookHandle <> 0 then
      inc(HookCount)
    else
      Result := FALSE;
  end else
    inc(HookCount);
end;

{ Call RemoveHook to remove the system hook. }
function RemoveHook: boolean; export;
begin
  { See if our reference count is down to 0, and if so then unhook. }
  Result := FALSE;
  if HookCount < 1 then exit;
  Result := TRUE;
  dec(HookCount);
  if HookCount = 0 then
    Result := UnhookWindowsHookEx(HookHandle);
end;

{ Have we hooked into the system? }
function IsHookSet: boolean; export;
begin
  Result := (HookCount > 0) and (HookHandle <> 0);
end;

exports
  InstallHook,
  RemoveHook,
  IsHookSet,
  MouseHookCallBack;

{ Initialize DLL data. }
begin
  HookCount := 0;
  HookHandle := 0;
end.

(* Then have this importation unit: *)

{ This is a simple DLL import unit to give us access to the functions in
  the HOOKDLL.PAS file.  This is the unit your project will use.}
unit Hookunit;

interface

uses WinTypes;

function InstallSystemHook: boolean;
function InstallTaskHook: boolean;
function RemoveHook: boolean;
function IsHookSet: boolean;
{ Do not use InstallHook directly.  Use InstallSystemHook or InstallTaskHook. }
function InstallHook(SystemHook: boolean; TaskHandle: THandle): boolean;

implementation

uses WinProcs;

const
  HOOK_DLL = 'HOOKDLL.DLL';

function InstallHook(SystemHook: boolean; 
                     TaskHandle: THandle): boolean; external HOOK_DLL;
function RemoveHook: boolean; external HOOK_DLL;
function IsHookSet: boolean; external HOOK_DLL;

function InstallSystemHook: boolean;
begin
  InstallHook(TRUE, 0);
end;

function InstallTaskHook: boolean;
begin
  InstallHook(FALSE,
              {$IFDEF WIN32}
                GetCurrentThreadID
              {$ELSE}
                GetCurrentTask
              {$ENDIF}
             ); 
end;

end.
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         {This code is a bit specific, but shows the ideas of finding a
 window handle and hooking a procedure into it }

procedure SetMessageTrappingHook; stdcall;
var
  TheHandle : HWND;
  TheThread : DWORD;
begin
  TheHandle := FindWindow('Whatever',NIL);
  if TheHandle <> 0 then begin
    TheThread := GetWindowThreadProcessId(TheHandle,NIL);
    HookProcHandle := SetWindowsHookEx(WH_CALLWNDPROC,@CallWndProc,
                                       HInstance,TheThread);
    if HookProcHandle <> 0 then
       NewMessages:=0;
    else
      ShowMessage('Setting Hook Failed.');
  end else
    showmessage('Icon Author is not currently running.');
end;

{Code provided by Michael}                                                                                                                                                                                                                                                                                                                                          Topic   Setting a Window Hook   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             {Hooking the keyboard}

library Sendkey;

uses
 SysUtils, WinTypes, WinProcs, Messages, Classes, KeyDefs;

type
  { Error codes }
  TSendKeyError = (sk_None, sk_FailSetHook, sk_InvalidToken, sk_UnknownError);

  { exceptions }
  ESendKeyError = class(Exception);
  ESetHookError = class(ESendKeyError);
  EInvalidToken = class(ESendKeyError);

  { a TList descendant that know how to dispose of its contents }
  TMessageList = class(TList)
  public
    destructor Destroy; override;
  end;

destructor TMessageList.Destroy;
var
  i: longint;
begin
  { deallocate all the message records before discarding the list }
  for i := 0 to Count - 1 do
    Dispose(PEventMsg(Items[i]));
  inherited Destroy;
end;

var
  { variables global to the DLL }
  MsgCount: word;
  MessageBuffer: TEventMsg;
  HookHandle: hHook;
  Playing: Boolean;
  MessageList: TMessageList;
  AltPressed, ControlPressed, ShiftPressed: Boolean;
  NextSpecialKey: TKeyString;

function MakeWord(L, H: Byte): Word;
   Topic   Setting up a Keyboard Hook   Language   P                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        { macro creates a word from low and high bytes }
inline(
  $5A/            { pop dx }
  $58/            { pop ax }
  $8A/$E2);       { mov ah, dl }

procedure StopPlayback;
{ Unhook the hook, and clean up }
begin
  { if Hook is currently active, then unplug it }
  if Playing then
    UnhookWindowsHookEx(HookHandle);
  MessageList.Free;
  Playing := False;
end;

function Play(Code: integer; wParam: word; lParam: Longint): Longint; export;
{This is the JournalPlayback callback function.  It is called by Windows
 when Windows polls for hardware events.  The code parameter indicates what 
 to do. }
begin
  case Code of

    hc_Skip: begin
    { hc_Skip means to pull the next message out of our list. If we 
      are at the end of the list, it's okay to unhook the JournalPlayback 
      hook from here. }
      { increment message counter }
      inc(MsgCount);
      { check to see if all messages have been played }
      if MsgCount >= MessageList.Count then
        StopPlayback
      else
      { copy next message from list into buffer }
      MessageBuffer := TEventMsg(MessageList.Items[MsgCount]^);
      Result := 0;
    end;

    hc_GetNext: begin
    { hc_GetNext means to fill the wParam and lParam with the proper 
      values so that the message can be played back.  DO NOT unhook 
      hook from within here.  Return value indicates how much time until 
      Windows should playback message.  We'll return 0 so that it's 
      processed right away. }
      { move message in buffer to message queue }
      PEventMsg(lParam)^ := MessageBuffer;
      Result := 0  { process immediately }
    end

    else
      { if Code isn't hc_Skip or hc_GetNext, then call next hook in chain }
      Result := CallNextHookEx(HookHandle, Code, wParam, lParam);
  end;
end;

procedure StartPlayback;
{ Initializes globals and sets the hook }
begin
  { grab first message from list and place in buffer in case we 
    get a hc_GetNext before and hc_Skip }
  MessageBuffer := TEventMsg(MessageList.Items[0]^);
  { initialize message count and play indicator }
  MsgCount := 0;
  { initialize Alt, Control, and Shift key flags }
  AltPressed := False;
  ControlPressed := False;
  ShiftPressed := False;
  { set the hook! }
  HookHandle := SetWindowsHookEx(wh_JournalPlayback, Play, hInstance, 0);
  if HookHandle = 0 then
    raise ESetHookError.Create('Couldn''t set hook')
  else
    Playing := True;
end;

procedure MakeMessage(vKey: byte; M: word);
{ procedure builds a TEventMsg record that emulates a keystroke and 
  adds it to message list }
var
  E: PEventMsg;
begin
  New(E);                                 { allocate a message record }
  with E^ do begin
    Message := M;                         { set message field }
    { high byte of ParamL is the vk code, low byte is the scan code }
    ParamL := MakeWord(vKey, MapVirtualKey(vKey, 0));
    ParamH := 1;                          { repeat count is 1 }
    Time := GetTickCount;                 { set time }
  end;
  MessageList.Add(E);
end;

procedure KeyDown(vKey: byte);
{ Generates KeyDownMessage }
begin
  { don't generate a "sys" key if the control key is pressed (Windows quirk) }
  if (AltPressed and (not ControlPressed) and (vKey in [Ord('A')..Ord('Z')])) or
     (vKey = vk_Menu) then
    MakeMessage(vKey, wm_SysKeyDown)
  else
    MakeMessage(vKey, wm_KeyDown);
end;

procedure KeyUp(vKey: byte);
{ Generates KeyUp message }
begin
  { don't generate a "sys" key if the control key is pressed (Windows quirk) }
  if AltPressed and (not ControlPressed) and (vKey in [Ord('A')..Ord('Z')]) then
    MakeMessage(vKey, wm_SysKeyUp)
  else
    MakeMessage(vKey, wm_KeyUp);
end;

procedure SimKeyPresses(VKeyCode: Word);
{ This function simulates keypresses for the given key, taking into 
  account the current state of Alt, Control, and Shift keys }
begin
  { press Alt key if flag has been set }
  if AltPressed then
    KeyDown(vk_Menu);
  { press Control key if flag has been set }
  if ControlPressed then
    KeyDown(vk_Control);
  { if shift is pressed, or shifted key and control is not pressed... }
  if (((Hi(VKeyCode) and 1) <> 0) and (not ControlPressed)) or ShiftPressed then
    KeyDown(vk_Shift);    { ...press shift }
  KeyDown(Lo(VKeyCode));  { press key down }
  KeyUp(Lo(VKeyCode));    { release key }
  { if shift is pressed, or shifted key and control is not pressed... }
  if (((Hi(VKeyCode) and 1) <> 0) and (not ControlPressed)) or ShiftPressed then
    KeyUp(vk_Shift);      { ...release shift }
  { if shift flag is set, reset flag }
  if ShiftPressed then begin
    ShiftPressed := False;
  end;
  { Release Control key if flag has been set, reset flag }
  if ControlPressed then begin
    KeyUp(vk_Control);
    ControlPressed := False;
  end;
  { Release Alt key if flag has been set, reset flag }
  if AltPressed then begin
    KeyUp(vk_Menu);
    AltPressed := False;
  end;
end;

procedure ProcessKey(S: String);
{ This function parses each character in the string to create the message list }
var
  KeyCode: word;
  Key: byte;
  index: integer;
  Token: TKeyString;
begin
  index := 1;
  repeat
    case S[index] of

      KeyGroupOpen : begin
      { It's the beginning of a special token! }
        Token := '';
        inc(index);
        while S[index] <> KeyGroupClose do begin
          { add to Token until the end token symbol is encountered }
          Token := Token + S[index];
          inc(index);
          { check to make sure the token's not too long }
          if (Length(Token) = 7) and (S[index] <> KeyGroupClose) then
            raise EInvalidToken.Create('No closing brace');
        end;
        { look for token in array, Key parameter will 
          contain vk code if successful }
        if not FindKeyInArray(Token, Key) then
          raise EInvalidToken.Create('Invalid token');
        { simulate keypress sequence }
        SimKeyPresses(MakeWord(Key, 0));
      end;

      AltKey : AltPressed := True; { set Alt flag }
      ControlKey : ControlPressed := True; { set Alt flag }

      ShiftKey : ShiftPressed := True; { set Alt flag }

      else begin
        {A normal character was pressed convert character into 
         a word where the high byte contains the shift state
         and the low byte contains the vk code }
        KeyCode := vkKeyScan(MakeWord(Byte(S[index]), 0));
        { simulate keypress sequence }
        SimKeyPresses(KeyCode);
      end;
    end;
    inc(index);
  until index > Length(S);
end;

function SendKeys(S: String): TSendKeyError; export;
{This is the one entry point.  Based on the string passed in the S  
 parameter, this function creates a list of keyup/keydown messages, 
 sets a JournalPlayback hook, and replays the keystroke messages.}
var
  i: byte;
begin
  try
    Result := sk_None;                   { assume success }
    MessageList := TMessageList.Create;  { create list of messages }
    ProcessKey(S);                       { create messages from string}
    StartPlayback;                       { set hook and play back messages }
  except
    { if an exception occurs, return an error code, and clean up }
    on E:ESendKeyError do begin
      MessageList.Free;
      if E is ESetHookError then
        Result := sk_FailSetHook
      else if E is EInvalidToken then
        Result := sk_InvalidToken;
    end else
      {Catch-all exception handler ensures than an exception 
       doesn't walk up into application stack }
      Result := sk_UnknownError;
  end;
end;

exports
  SendKeys index 1;

begin
end
