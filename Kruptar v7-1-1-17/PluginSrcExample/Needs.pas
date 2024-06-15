unit Needs;

interface

type
 TMethod = (tmNormal, tmL1T, tmL1Te, tmL1TL1T, tmC2S1eSne, tmHat, tmBkp, tmP2P, tmNone);
 DWord = LongWord;
 PByte = ^Byte;
 PWord = ^Word;
 PDWord = ^DWord;
 PBytes = ^TBytes;
 TBytes = Array[Word] of Byte;
 PWords = ^TWords;
 TWords = Array[Word] of Word;
 PDWords = ^TDWords;
 TDWords = Array[Word] of DWord;
 TItemState = (isCode, isBreak, isTerminate);
 PTableItem = ^TTableItem;
 TTableItem = record
  tiCodes: AnsiString;
  tiString: WideString;
  tiState: TItemState;
  Next: PTableItem;
 end;
 PTextString = ^TTextString;
 TTextString = record
  Str: AnsiString;
  Next: PTextString;
 end;
 PTextStrings = ^TTextStrings;
 TTextStrings = object
  Root, Cur: PTextString;
  Count: LongInt;
  constructor Init;
  function Add: PTextString;
  function Get(I: LongInt): PTextString;
  destructor Done;
 end;

function CompareMem(P1, P2: Pointer; Length: LongInt): Boolean;
procedure DisposeStrings(TextStrings: PTextStrings); stdcall;
function GetEnd(Data: Pointer; Root: PTableItem; Max: LongInt): PTableItem;

implementation

procedure DisposeStrings(TextStrings: PTextStrings); stdcall;
begin
 Dispose(TextStrings, Done);
end;

function CompareMem(P1, P2: Pointer; Length: LongInt): Boolean; assembler;
asm
        PUSH    ESI
        PUSH    EDI
        MOV     ESI,P1
        MOV     EDI,P2
        MOV     EDX,ECX
        XOR     EAX,EAX
        AND     EDX,3
        SHR     ECX,1
        SHR     ECX,1
        REPE    CMPSD
        JNE     @@2
        MOV     ECX,EDX
        REPE    CMPSB
        JNE     @@2
        INC     EAX
@@2:    POP     EDI
        POP     ESI
end;

function TTextStrings.Get(I: LongInt): PTextString;
var
 N: PTextString;
 J: LongInt;
begin
 N := Root;
 J := 0;
 while N <> NIL do
 begin
  If J = I then
  begin
   Result := N;
   Exit;
  end;
  Inc(J);
  N := N^.Next;
 end;
 Result := NIL;
end;

constructor TTextStrings.Init;
begin
 Root := NIL;
 Cur := NIL;
 Count := 0;
end;

function TTextStrings.Add: PTextString;
begin
 New(Result);
 If Root = NIL then Root := Result Else Cur^.Next := Result;
 Cur := Result;
 Inc(Count);
 Result^.Str := '';
 Result^.Next := NIL;
end;

destructor TTextStrings.Done;
var
 N: PTextString;
begin
 while Root <> NIL do
 begin
  N := Root^.Next;
  Root^.Str := '';
  Dispose(Root);
  Root := N;
 end;
 Count := 0;
 Cur := NIL;
end;

function GetEnd(Data: Pointer; Root: PTableItem; Max: LongInt): PTableItem;
var
 EF: Boolean;
 J: LongInt;
begin
 EF := False;
 J := Max;
 repeat
  Result := Root;
  while Result <> NIL do With Result^ do
  begin
   If (Length(tiCodes) = J) and CompareMem(Data, Addr(tiCodes[1]), J) then
   begin
    EF := True;
    Break;
   end;
   Result := Next;
  end;
  Dec(J);
 until EF or (J = 0);
end;

end.
