macro twice(expression)
    :($expression, $expression)
end

@twice println("Hello")