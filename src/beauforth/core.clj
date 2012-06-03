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

(define "clj" (comp eval read))

(defn evaluate [s]
  (if-let [f (lookup s)]
    (f)
    (println "push(datastack," s ")")))

(defn -main
  "I don't do a whole lot."
  [& args]
  (doseq [w (wordseq)]
    (evaluate w)))
