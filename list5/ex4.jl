file = open("ex3.jl", "r")

characterCount = Dict()

while !eof(file)
    line = readline(file)
    foreach((char) -> char in keys(characterCount) ? characterCount[char] += 1 : characterCount[char] = 1, line)
end

close(file)

println(characterCount)