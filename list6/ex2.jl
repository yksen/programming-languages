using Test

simplify(n::Union{Number,Symbol}) = n
function simplify(expr::Expr)
    if expr.head == :call
        args = simplify.(expr.args[2:end])
        operator = expr.args[1]

        if operator == :+
            numbersSum = sum([arg for arg in args if isa(arg, Number)])
            nonNumbers = filter(x -> !isa(x, Number), args)

            if numbersSum != 0
                push!(nonNumbers, numbersSum)
            end

            if length(nonNumbers) == 1
                return nonNumbers[1]
            elseif isempty(nonNumbers)
                return 0
            else
                return Expr(:call, :+, nonNumbers...)
            end

        elseif operator == :*
            numbersProduct = prod([arg for arg in args if isa(arg, Number)])
            nonNumbers = filter(x -> !isa(x, Number), args)

            if numbersProduct == 0
                return 0
            elseif numbersProduct != 1
                push!(nonNumbers, numbersProduct)
            end

            if length(nonNumbers) == 1
                return nonNumbers[1]
            elseif isempty(nonNumbers)
                return 1
            else
                return Expr(:call, :*, nonNumbers...)
            end
        end
    end

    return expr
end

@test simplify(1 + 2) == 3
@test simplify(:(1 + 2 + 4)) == 7
@test simplify(:(x + 0 + y)) == :(x + y)
@test simplify(:(x * y * 0)) == 0
@test simplify(:(x + 0)) == :x
@test simplify(:(x + 3 + y + 5)) == :(x + y + 8)
@test simplify(:(x * 3 * y * 5)) == :(x * y * 15)