struct Dot
    x::Float64
    y::Float64
end

struct Circle
    center::Dot
    radius::Float64
end

struct Rectangle
    center::Dot
    width::Float64
    height::Float64
end

area(d::Dot) = 0
area(c::Circle) = pi * c.radius^2
area(r::Rectangle) = r.width * r.height

distance(d1::Dot, d2::Dot) = sqrt((d1.x - d2.x)^2 + (d1.y - d2.y)^2)
distance(d::Dot, c::Circle) = distance(d, c.center) - c.radius
function distance(d::Dot, r::Rectangle)
    horizontalDistance = abs(d.x - r.center.x) - r.width / 2
    verticalDistance = abs(d.y - r.center.y) - r.height / 2 
    if horizontalDistance <= 0
        return verticalDistance
    elseif verticalDistance <= 0
        return horizontalDistance
    else
        return sqrt(horizontalDistance^2 + verticalDistance^2)
    end
end

distance(c1::Circle, c2::Circle) = distance(c1.center, c2.center) - c1.radius - c2.radius
distance(c::Circle, d::Dot) = distance(d, c)
distance(c::Circle, r::Rectangle) = 

distance(r1::Rectangle, r2::Rectangle) = distance(r1.center, r2)
distance(r::Rectangle, d::Dot) = distance(d, r)
# distance(r::Rectangle, c::Circle) = distance(c, r)

println(distance(Rectangle(Dot(0, 0), 2, 2), Rectangle(Dot(3, 3), 2, 2)))