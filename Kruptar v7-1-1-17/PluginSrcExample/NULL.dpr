library NULL;

uses
  ShareMem,
  Needs;

{$E .kpl}

const
 SKPLDescription = 'NULL';

Var
 ROM: PBytes = NIL;
 RomSize: LongInt = 0;
 EndsRoot: PTableItem = NIL;
 MaxCodes: LongInt = 0;
 Align: LongInt = 1;

Function Description: AnsiString; stdcall;
begin
 Result := SKPLDescription;
end;

Function NeedEnd: LongInt; stdcall;
begin
 Result := 0;
end;

Function GetMethod: TMethod; stdcall;
begin
 Result := tmNone;
end;

Procedure SetVariables(X: PBytes; Sz: LongInt; ER: PTableItem; MC, AL: LongInt); stdcall;
begin
 ROM := X;
 RomSize := Sz;
 EndsRoot := ER;
 MaxCodes := MC;
 Align := AL;
end;

Function GetData(TextStrings: PTextStrings): AnsiString; stdcall;
Var R: PTextString;
begin
 Result := '';
 If TextStrings = NIL then Exit;
 With TextStrings^ do
 begin
  R := Root;
  While R <> NIL do
  begin
   Result := Result + R^.Str + #0;
   R := R^.Next;
  end;
 end;
end;

Function GetStrings(X, Sz: LongInt): PTextStrings; stdcall;
Var P: PChar;
begin
 Result := NIL;
 If (X >= RomSize) or (X < 0) then Exit;
 New(Result, Init);
 With Result^.Add^ do
 begin
  P := Addr(ROM^[X]);
  Str := '';
  While P^ <> #0 do
  begin
   Str := Str + P^;
   Inc(P);
  end;
 end;
end;

exports
 GetMethod,
 SetVariables,
 GetData,
 GetStrings,
 DisposeStrings,
 NeedEnd,
 Description;

end.

