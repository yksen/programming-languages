t = rand(1:15, 20, 40)

dictionaryCount = Dict(x => 0 for x in 1:15)
foreach((x) -> dictionaryCount[x] += 1, t)
println(dictionaryCount)

arrayCount = zeros(15)
foreach((x) -> arrayCount[x] += 1, t)
println(arrayCount)