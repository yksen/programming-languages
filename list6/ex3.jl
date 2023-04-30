using Test
include("ex2.jl")

derivative(n::Number) = 0
derivative(n::Symbol) = n == :x ? 1 : 0

function derivative(expr::Expr)
    if expr.head == :call
        if expr.args[1] == :+
            return Expr(:call, :+, derivative(expr.args[2]), derivative(expr.args[3]))
        elseif expr.args[1] == :-
            return Expr(:call, :-, derivative(expr.args[2]), derivative(expr.args[3]))
        elseif expr.args[1] == :*
            return Expr(:call, :+, Expr(:call, :*, expr.args[2], derivative(expr.args[3])), Expr(:call, :*, expr.args[3], derivative(expr.args[2])))
        elseif expr.args[1] == :/
            return Expr(:call, :/, Expr(:call, :-, Expr(:call, :*, derivative(expr.args[2]), expr.args[3]), Expr(:call, :*, expr.args[2], derivative(expr.args[3]))), Expr(:call, :^, expr.args[3], 2))
        elseif expr.args[1] == :^
            if expr.args[2] == :x
                return Expr(:call, :*, expr.args[3], Expr(:call, :^, :x, Expr(:call, :-, expr.args[3], 1)))
            elseif expr.args[3] == :x
                return Expr(:call, :*, expr, Expr(:call, :log, expr.args[2]))
            end
        elseif expr.args[1] == :sin
            return Expr(:call, :cos, expr.args[2])
        elseif expr.args[1] == :cos
            return Expr(:call, :-, Expr(:call, :sin, expr.args[2]))
        elseif expr.args[1] == :tan
            return Expr(:call, :/, 1, Expr(:call, :^, Expr(:call, :cos, expr.args[2]), 2))
        elseif expr.args[1] == :log
            return Expr(:call, :/, 1, expr.args[2])
        elseif expr.args[1] == :exp
            return expr
        elseif expr.args[1] == :sqrt
            return Expr(:call, :/, 1, Expr(:call, :*, 2, Expr(:call, :sqrt, expr.args[2])))
        end
    elseif expr == x
        return 1
    elseif typeof(expr) <: Number || expr isa Symbol
        return 0
    end
end

println(simplify(derivative(1)))
println(simplify(derivative(:x)))
println(simplify(derivative(:y)))
println(simplify(derivative(:(x + 1))))
println(simplify(derivative(:(x + x^2))))
println(simplify(derivative(:(x * y))))
println(simplify(derivative(:(x / y))))
println(simplify(derivative(:(x^2))))
println(simplify(derivative(:(sin(x)))))
println(simplify(derivative(:(cos(x)))))
println(simplify(derivative(:(tan(x)))))
println(simplify(derivative(:(log(x)))))
println(simplify(derivative(:(exp(x)))))

while true
    println("Podaj wzór funkcji:")
    input = readline()
    expr = Meta.parse(input)
    println("Pochodna funkcji to:")
    println(simplify(derivative(expr)))
end