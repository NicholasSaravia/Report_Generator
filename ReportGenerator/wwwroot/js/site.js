function ScrollToScores() {
    setTimeout(() => {
        document.getElementById("end").scrollIntoView({ behavior: "smooth", block: "end", inline: "nearest" });
    }, 500);
}