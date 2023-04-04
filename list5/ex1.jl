struct Person
    firstName::String
    lastName::String
end

people = [
    Person("Jan", "Kowalski"),
    Person("Ewa", "Nowak"),
    Person("Artur", "Kowalski"),
    Person("Adam", "Nowak"),
]

println("(a)")
println(sort(people, by = p -> (p.firstName)))
println(sort(people, by = p -> (p.firstName), rev = true))

println("(b)")
println(sort(people, lt = (p1, p2) -> p1.firstName < p2.firstName))
println(sort(people, lt = (p1, p2) -> p1.firstName > p2.firstName))

println("(c)")
println(sort(people, by = p -> (p.lastName, p.firstName)))
println(sort(people, by = p -> (p.lastName, p.firstName), rev = true))

println("(d)")
println(sort(people, lt = (p1, p2) -> p1.lastName < p2.lastName || (p1.lastName == p2.lastName && p1.firstName < p2.firstName)))
println(sort(people, lt = (p1, p2) -> p1.lastName > p2.lastName || (p1.lastName == p2.lastName && p1.firstName > p2.firstName)))