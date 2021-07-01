FUNCTION_BLOCK TestProgramm
VAR_INPUT
END_VAR
VAR_OUTPUT
END_VAR
VAR
    Zeitgeber: TON_TIME;
    ZyklusAktiv: Bool;
END_VAR

(*  *********************************************************** *)
(*  Blinklicht Fibonacci                                        *)
(*  *********************************************************** *)

#Zeitgeber(IN := #ZyklusAktiv,
           PT := T#4s);

IF "DigInput".S1 THEN
    #ZyklusAktiv := TRUE;
END_IF;

IF #Zeitgeber.Q THEN
    #ZyklusAktiv := FALSE;
END_IF;

"DigOutput".P1 := #ZyklusAktiv
    AND (
            (#Zeitgeber.ET < T#100MS)
        OR  (#Zeitgeber.ET > T#200MS AND #Zeitgeber.ET < T#400MS)
        OR  (#Zeitgeber.ET > T#700MS AND #Zeitgeber.ET < T#1S200MS)
        OR  (#Zeitgeber.ET > T#2S AND #Zeitgeber.ET < T#3S300MS)
        );
        
    