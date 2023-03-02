function BST(key, left, right) {
    this.key = key;
    this.left = left;
    this.right = right;
}

BST.prototype[Symbol.iterator] = function* () {
    if (this.left)
        yield* this.left;
    yield this.key;
    if (this.right)
        yield* this.right;
}

let tree = new BST(5, new BST(3, new BST(2), new BST(4)), new BST(10, new BST(8), new BST(12)));
for (x of tree)
    console.log(x);