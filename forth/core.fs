clj
(beauforth.core/define ":*"
  (fn []
    (let [w (beauforth.core/word)
          exp (read)]
      (beauforth.core/define w
        (fn [] (eval exp))))))

:* : (let [w (beauforth.core/word)]
       (println (str (munge w) ":"))
       (beauforth.core/define w #(beauforth.core/println-to-main "rcall" (munge w))))

:* ; (println "ret\n")

:* asm (println (read-line))

: +
asm bf_pop(r16)
asm bf_pop(r17)
asm add r16, r17
asm bf_push(r16)
;

: dup
asm bf_pop(r16)
asm bf_push(r16)
asm bf_push(r16)
;

: led
asm bf_pop(r16)
asm cpi r16, 10
asm brne loop

asm sbi _SFR_IO_ADDR(DDRD),6
asm sbi _SFR_IO_ADDR(PORTD),6

5 dup + led
