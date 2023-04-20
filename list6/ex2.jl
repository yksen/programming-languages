using Test

simplify(n::Union{Number,Symbol}) = n
function simplify(e::Expr)
    args = e.args[2:end]
    args = map(simplify, args)
    op = e.args[1]
    if op == :+
        s = sum(filter(x -> x isa Number, args))
        symbols = filter(x -> !(x isa Number), args)
        if s == 0 && length(symbols) == 1
            return :($args)
        elseif s != 0 && length(symbols) == 0
            return :($s)
        elseif s != 0 && length(symbols) > 0
            return Expr(:call, :+, symbols..., s)
        else
            return Expr(:call, :+, symbols...)
        end
    end
    if op == :*
        p = prod(filter(x -> x isa Number, args))
        symbols = filter(x -> !(x isa Number), args)
        if p == 0
            return :($p)
        else
            return Expr(:call, :*, symbols...,  p)
        end
    end
end

@test simplify(1 + 2) == 3
@test simplify(:(1 + 2 + 4)) == 7
@test simplify(:(x + 0 + y)) == :(x + y)
@test simplify(:(x * y * 0)) == 0
@test simplify(:(x + 0)) == :x
@test simplify(:(x + 3 + y + 5)) == :(x + y + 8)
@test simplify(:(x * 3 * y * 5)) == :(x * y * 15)