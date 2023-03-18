const http = require("http");
const fs = require("fs");
const { userInfo } = require("os");

const PORT = 8080;

var paths = {
    "/": "index.html",
    "/a": "a.html",
    "/b": "b.html",
    "/c": "c.html",
    "/image.png": "image.png",
    "/jpqui-lista4-2023.pdf": "../jpqui-lista4-2023.pdf",
};

var useReadFileSync = false;

http.createServer((request, response) => {
    let path = paths[request.url];
    if (path == undefined) {
        response.writeHead(404, { "Content-Type": "text/html" });
    } else if (path.endsWith(".pdf")) {
        response.writeHead(200, { "Content-Type": "application/pdf" });
    } else if (path.endsWith(".png")) {
        response.writeHead(200, { "Content-Type": "image/png" });
    } else {
        response.writeHead(200, { "Content-Type": "text/html" });
    }

    let content = "<h1>404 Not Found</h1>";
    if (useReadFileSync) {
        if (path != undefined)
            content = fs.readFileSync(path);
        response.end(content);
    } else {
        if (path == undefined)
            response.end(content);
        else
            fs.readFile(path, (error, data) => {
                if (error) throw error;
                response.end(data);
            });
    }

}).listen(PORT);

console.log(`Listening on port ${PORT}`);