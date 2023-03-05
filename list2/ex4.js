Array.prototype.wspak = function* () {
    for (let i = this.length - 1; i >= 0; --i)
        yield this[i];
}

for (let x of [2, 3, 4, 5].wspak())
    console.log(x);