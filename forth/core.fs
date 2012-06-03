clj
(beauforth.core/define ":*"
  (fn []
    (let [w (beauforth.core/word) exp (read)]
      (beauforth.core/define w
        (fn [] (eval exp))))))

:* : (let [w (beauforth.core/word)]
       (print "subroutine ")
       (println w)
       (println "push(returnstack, nextword)")
       (beauforth.core/define w #(println "subcall" w "nextword")))

:* ; (do
       (println "pop(returnstack, nextword)")
       (println "subret nextword")
       (println "ends"))

:* nbc (println (read-line))

: +
nbc pop(datastack, val1)
nbc pop(datastack, val2)
nbc add val1, val2, val3
nbc push(datastack, val3)
;

: -
nbc pop(datastack, val1)
nbc pop(datastack, val2)
nbc sub val1, val2, val3
nbc push(datastack, val3)
;

: *
nbc pop(datastack, val1)
nbc pop(datastack, val2)
nbc mul val1, val2, val3
nbc push(datastack, val3)
;

: dup
nbc pop(datastack, val1)
nbc push(datastack, val1)
nbc push(datastack, val1)
;

1 dup *
