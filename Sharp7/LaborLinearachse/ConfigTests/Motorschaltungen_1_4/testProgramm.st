FUNCTION_BLOCK TestProgramm
VAR_INPUT
END_VAR
VAR_OUTPUT
END_VAR
VAR
    BlinkMerker: BOOL;
    StörungAktiv: BOOL;
    StörungNochNichtQuittiert: BOOL;
	BlinkTimer: TON;
END_VAR

(*  *********************************************************** *)
(*  Linearachse: Motorschaltungen 1 - Version 4                 *)
(*  *********************************************************** *)

#BlinkTimer.TON(IN := NOT #BlinkTimer.Q,
PT := T#1250ms);

#BlinkMerker := #BlinkTimer.ET < T#625ms;

IF "DigInput".S1 AND "DigInput".S2 AND NOT #StörungNochNichtQuittiert THEN
    "DigOutput".P4 := true;
END_IF;

IF NOT "DigInput".S2 OR #StörungNochNichtQuittiert THEN
    "DigOutput".P4 := FALSE;
END_IF;

IF NOT "DigInput".S9 OR NOT "DigInput".S10 THEN
    #StörungAktiv := true;
    #StörungNochNichtQuittiert := true;
END_IF;

IF "DigInput".S9 AND "DigInput".S10 THEN
    #StörungAktiv := false;
END_IF;

IF (NOT #StörungAktiv AND "DigInput".S8) THEN
    #StörungNochNichtQuittiert := false;
END_IF;

"DigOutput".P3 := #StörungAktiv OR (#StörungNochNichtQuittiert AND #BlinkMerker);

"DigOutput".P2 := NOT "DigOutput".P4 AND NOT #StörungNochNichtQuittiert;
    