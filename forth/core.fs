clj
(beauforth.core/define ":*"
  (fn []
    (let [w (beauforth.core/word)
          exp (read)]
      (beauforth.core/define w
        (fn [] (eval exp))))))

:* : (let [w (beauforth.core/word)]
       (println "subroutine" (munge w))
       (println "push(returnstack, nextword)")
       (beauforth.core/define w #(beauforth.core/println-to-main "subcall" (munge w) "nextword")))

:* ; (do
       (println "pop(returnstack, nextword)")
       (println "subret nextword")
       (println "ends"))

:* nbc (println (read-line))

: dot
nbc pop(datastack, val1)
nbc NumOut(0, 0, val1)
;

: sleep
nbc pop(datastack, val1)
nbc Wait(val1)
;

: +
nbc pop(datastack, val1)
nbc pop(datastack, val2)
nbc add val3, val1, val2
nbc push(datastack, val3)
;

: -
nbc pop(datastack, val1)
nbc pop(datastack, val2)
nbc sub val3, val1, val2
nbc push(datastack, val3)
;

: *
nbc pop(datastack, val1)
nbc pop(datastack, val2)
nbc mul val3, val1, val2
nbc push(datastack, val3)
;

: dup
nbc pop(datastack, val1)
nbc push(datastack, val1)
nbc push(datastack, val1)
;

2 dup * dot
1000 sleep
