using Plots

display(plot(show=true, aspect_ratio=:equal, legend=:outerright, xlabel="x", ylabel="y", title="Functions"))
xlims!(-5, 5)
ylims!(-5, 5)

plot!(x -> -x, -5, 5, label="-x", linewidth=3, ls=:dot)
plot!(x -> x^3 / (x^2 + 1), -5, 5, label="x^3 / (x^2 + 1)", linewidth=5, ls=:dash)
plot!(x -> sin(x), -5, 5, label="sin(x)", linewidth=7, ls=:dashdot)

while true
    print("f(x) = ")
    input = readline()
    f = eval(Meta.parse("x -> $input"))
    
    x = -5:0.01:5
    y = map(f, x)
    
    plot!(x, y, label="$input")
end
