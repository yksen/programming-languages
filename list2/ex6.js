function arithmetic_seq({ ...data }) {
    let args = Object.keys(data);
    let a = args.filter(arg => arg.startsWith('a'));
    let s = args.filter(arg => arg.startsWith("sum"));
    let r = data.r;
    let a1;

    if (r != undefined && a.length == 1) {
        [ax, n] = [data[a[0]], a[0].slice(1)];
        a1 = ax - r * (n - 1);
    }
    else if (a.length == 2) {
        [ax, ay, n1, n2] = [data[a[0]], data[a[1]], a[0].slice(1), a[1].slice(1)];
        r = (ay - ax) / (n2 - n1);
        a1 = ax - r * (n1 - 1);
    }
    else if (s.length == 1 && r != undefined) {
        [sx, n] = [data[s[0]], s[0].slice(3)];
        a1 = (2 * sx / n - r * (n - 1)) / 2;
    }
    else if (s.length == 2) {
        [sx, sy, n1, n2] = [data[s[0]], data[s[1]], s[0].slice(3), s[1].slice(3)];
        r = (2 * n2 * sx - 2 * n1 * sy) / (n1 ** 2 * n2 - n1 * n2 ** 2);
        a1 = (2 * sx / n1 - r * (n1 - 1)) / 2;
    }
    else if (s.length == 1 && a.length == 1) {
        [sx, ax, n1, n2] = [data[s[0]], data[a[0]], s[0].slice(3), a[0].slice(1)];
        r = (2 * sx - 2 * ax * n1) / (n1 * (n1 - 2 * n2 + 1));
        a1 = (2 * sx / n1 - r * (n1 - 1)) / 2;
    }

    return {
        a(i) { return a1 + r * (i - 1); },
        sum(i) { return (a1 + this.a(i)) * i / 2; },
        get r() { return r; },
        *[Symbol.iterator]() {
            let i = 1;
            while (a1 != undefined && r != undefined) {
                yield this.a(i++);
            }
        },
    }
}

function test(object) {
    let iter = object[Symbol.iterator]();
    for (let i = 0; i < 5; i++) {
        process.stdout.write(iter.next().value + " ");
    }
    console.log();
}

test(arithmetic_seq({ a7: 9, r: 2 }));
test(arithmetic_seq({ a3: 8, a5: 2 }));
test(arithmetic_seq({ sum5: 15, r: 1 }));
test(arithmetic_seq({ sum3: 12, sum6: 42 }));
test(arithmetic_seq({ sum5: 20, a2: 13 }));