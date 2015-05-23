(ns beauforth.core
  (:use [clojure.string :only [blank?]]))

(defn read-char []
  (let [ch (.read *in*)]
    (if (= -1 ch)
      nil
      (char ch))))

(defn wordseq []
  (->> #(read-char)
    repeatedly
    (take-while identity)
    (partition-by #(Character/isWhitespace %))
    (map #(apply str %))
    (remove blank?)))

(defn word []
  (first (wordseq)))

(def definitions (atom ())) ; might switch to map

(defn lookup [sym]
  (some #(when (= sym (first %)) (peek %)) @definitions))

(defn define [sym f]
  (swap! definitions conj [sym f]))

(let [main (new java.io.StringWriter)]
  (defn println-to-main [& s]
    (binding [*out* main]
      (apply println s)))

  (defn print-main []
    (println "thread:")
    (println (str main))
    (println "loop: rjmp loop")))

(define "clj" (comp eval read))

(defn evaluate [s]
  (if-let [f (lookup s)]
    (f)
    (do
      (println-to-main "ldi r16," s)
      (println-to-main "bf_push(r16)"))))

(defn -main
  "I don't do a whole lot."
  [& args]
  (println "#include \"forth/forthlib.S\"")
  (doseq [w (wordseq)]
    (evaluate w))
  (print-main))

