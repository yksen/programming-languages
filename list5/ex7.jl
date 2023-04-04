struct PrimesUpTo
    n::Int
end

function Base.iterate(p::PrimesUpTo, state=2)
    while state <= p.n
        if isPrime(state)
            return (state, state + 1)
        end
        state += 1
    end
    return nothing
end

isPrime(n::Int) = n > 1 && all(n % i != 0 for i in 2:(n - 1))

for i in PrimesUpTo(100)
    print(i, ' ')
end