using Test

simplify(n::Union{Number,Symbol}) = n
function simplify(e::Expr)
    args = map(simplify, e.args)
    op = e.args[1]
    if op == :+
        args = filter(x -> x != 0, args)
        if length(args) == 0
            return 0
        end
        return foldl(+, args[2:end])
    end
    if op == :*
        args = filter(x -> x != 1, args)
        if length(args) == 0
            return 1
        end
        return foldl(*, args[2:end])
    end
end

@test simplify(1 + 2) == 3
@test simplify(:(1 + 2 + 4)) == 7
@test simplify(:(x + 0 + y)) == :(x + y)
@test simplify(:(x * y * 0)) == 0
@test simplify(:(x + 0)) == :x
@test simplify(:(x + 3 + y + 5)) == :(x + y + 8)
@test simplify(:(x * 3 * y * 5)) == :(x * y * 15)