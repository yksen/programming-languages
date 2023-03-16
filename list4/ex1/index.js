const http = require("http");
const fs = require("fs");

const PORT = 8080;

var paths = {
    "/": "index.html",
    "/a": "a.html",
    "/b": "b.html",
    "/c": "c.html",
};

http.createServer((request, response) => {
    let path = paths[request.url];
    if (path == undefined) {
        response.writeHead(404, { "Content-Type": "text/html" });
        response.write("404 Not Found");
    } else {
        let html = fs.readFileSync(path);
        response.writeHead(200, { "Content-Type": "text/html" });
        response.write(html);
    }
    response.end();
}).listen(PORT);

console.log(`Listening on port ${PORT}`);