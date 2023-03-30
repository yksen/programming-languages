struct Dot
    center::Complex
end

struct Circle
    center::Dot
    radius::Unsigned
end

struct Rectangle
    center::Dot
    width::Unsigned
    height::Unsigned
end

area(d::Dot) = 0
area(c::Circle) = pi * c.radius^2
area(r::Rectangle) = r.width * r.height

border(d::Dot) = 0
border(c::Circle) = 2 * pi * c.radius
border(r::Rectangle) = 2 * (r.width + r.height)

distance(d1::Dot, d2::Dot) = abs(d1.center - d2.center)
distance(d::Dot, c::Circle) = max(0, distance(d, c.center) - c.radius)
function distance(d::Dot, r::Rectangle)
    horizontalDistance = abs(real(d.center) - real(r.center)) - r.width / 2
    verticalDistance = abs(imag(d.center) - imag(r.center)) - r.height / 2 
    if horizontalDistance <= 0
        return verticalDistance
    elseif verticalDistance <= 0
        return horizontalDistance
    else
        return sqrt(horizontalDistance^2 + verticalDistance^2)
    end
end

distance(c1::Circle, c2::Circle) = max(0, distance(c1.center, c2.center) - c1.radius - c2.radius)
distance(c::Circle, d::Dot) = distance(d, c)
# distance(c::Circle, r::Rectangle) = 

distance(r1::Rectangle, r2::Rectangle) = distance(r1.center, r2)
distance(r::Rectangle, d::Dot) = distance(d, r)
distance(r::Rectangle, c::Circle) = distance(c, r)

println(distance(Dot(0 + 0im), Dot(1 + 1im)))
println(distance(Dot(0 + 0im), Circle(Dot(1 + 1im), 1)))
println(distance(Dot(0 + 0im), Rectangle(Dot(1 + 1im), 1, 1)))