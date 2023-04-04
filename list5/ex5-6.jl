using Plots

struct Dot
    center::Complex
end

struct Circle
    center::Complex
    radius::Unsigned
end

struct Rectangle
    center::Complex
    width::Unsigned
    height::Unsigned
end

area(d::Dot) = 0
area(c::Circle) = pi * c.radius^2
area(r::Rectangle) = r.width * r.height

border(d::Dot) = 0
border(c::Circle) = 2 * pi * c.radius
border(r::Rectangle) = 2 * (r.width + r.height)

distance(n1::Complex, n2::Complex) = abs(n1 - n2)
distance(d1::Dot, d2::Dot) = distance(d1.center, d2.center)
distance(d::Dot, c::Circle) = max(0, distance(d.center, c.center) - c.radius)
function distance(d::Dot, r::Rectangle)
    horizontalDistance = abs(real(d.center) - real(r.center)) - r.width / 2
    verticalDistance = abs(imag(d.center) - imag(r.center)) - r.height / 2
    return sqrt(max(horizontalDistance, 0)^2 + max(verticalDistance, 0)^2)
end

distance(c1::Circle, c2::Circle) = max(0, distance(c1.center, c2.center) - c1.radius - c2.radius)
distance(c::Circle, d::Dot) = distance(d, c)
function distance(c::Circle, r::Rectangle)
    horizontalDistance = abs(real(c.center) - real(r.center)) - r.width / 2
    verticalDistance = abs(imag(c.center) - imag(r.center)) - r.height / 2
    return max(sqrt(max(horizontalDistance, 0)^2 + max(verticalDistance, 0)^2) - c.radius, 0)
end

function distance(r1::Rectangle, r2::Rectangle)
    horizontalDistance = abs(real(r1.center) - real(r2.center)) - r1.width / 2 - r2.width / 2
    verticalDistance = abs(imag(r1.center) - imag(r2.center)) - r1.height / 2 - r2.height / 2
    return sqrt(max(horizontalDistance, 0)^2 + max(verticalDistance, 0)^2)
end
distance(r::Rectangle, d::Dot) = distance(d, r)
distance(r::Rectangle, c::Circle) = distance(c, r)

draw(d::Dot) = scatter!([real(d.center)], [imag(d.center)])
draw(c::Circle) = plot!([real(c.center) .+ c.radius * cos(Θ) for Θ in range(0, 2π, length=100)], [imag(c.center) .+ c.radius * sin(Θ) for Θ in range(0, 2π, length=100)])
draw(r::Rectangle) = plot!([real(r.center) - r.width / 2, real(r.center) + r.width / 2, real(r.center) + r.width / 2, real(r.center) - r.width / 2, real(r.center) - r.width / 2],
                           [imag(r.center) - r.height / 2, imag(r.center) - r.height / 2, imag(r.center) + r.height / 2, imag(r.center) + r.height / 2, imag(r.center) - r.height / 2])

shapes = [Dot(0 + 0im), Dot(5 + 5im), Dot(-6 - 3im), Dot(3 - 2im),
          Circle(4 + 2im, 1), Circle(0 - 3im, 2), Circle(-3 + 3im, 3), Circle(2 + 2im, 4),
          Rectangle(-4 - 3im, 2, 2), Rectangle(-2 + 2im, 2, 6), Rectangle(3 - 2im, 10, 4), Rectangle(4 + 4im, 1, 2)]

display(plot(show=true, aspect_ratio=:equal))
foreach(draw, shapes)
display([distance(s1, s2) for s1 in shapes, s2 in shapes])
readline()