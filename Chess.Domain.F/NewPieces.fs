namespace Chess.Domain

module public NewPieces =
    let public sumpter = {
        Advances = Combinations.orthogonalKnightMoves |> Set.ofList
        Captures = Set.empty
    }
    let public destrier(mindset) = {
        Advances = Set.empty
        Captures = [
            for move in Combinations.orthogonalKnightMoves -> {
                Relocation = move
                Mindset = mindset
            }
        ] |> Set.ofList
    }
    let public knight(mindset) = {
        Advances = Combinations.orthogonalKnightMoves |> Set.ofList
        Captures = [
            for move in Combinations.orthogonalKnightMoves -> {
                Relocation = move
                Mindset = mindset
            }
        ] |> Set.ofList
    }
    let public palfrey = {
        Advances = Combinations.diagonalKnightMoves |> Set.ofList
        Captures = Set.empty
    }
    let public coursier(mindset) = {
        Advances = Set.empty
        Captures = [
            for move in Combinations.diagonalKnightMoves -> {
                Relocation = move
                Mindset = mindset
            }
        ] |> Set.ofList
    }
    let public paladin(mindset) = {
        Advances = Combinations.diagonalKnightMoves |> Set.ofList
        Captures = [
            for move in Combinations.diagonalKnightMoves -> {
                Relocation = move
                Mindset = mindset
            }
        ] |> Set.ofList
    }
    let public rook(mindset) = {
        Advances = List.concat [
            Combinations.orthogonalOneTileMoves
            Combinations.orthogonalTwoTileMoves
            Combinations.orthogonalThreeTileMoves
        ] |> Set.ofList
        Captures = [
            for move in List.concat [
                Combinations.orthogonalOneTileMoves
                Combinations.orthogonalTwoTileMoves
                Combinations.orthogonalThreeTileMoves
            ] -> {
                Relocation = move
                Mindset = mindset
            }
        ] |> Set.ofList
    }
    let public rookie(mindset) = {
        Advances = List.concat [
            Combinations.orthogonalOneTileMoves
            Combinations.orthogonalTwoTileMoves
        ] |> Set.ofList
        Captures = [
            for move in List.concat [
                Combinations.orthogonalOneTileMoves
                Combinations.orthogonalTwoTileMoves
            ] -> {
                Relocation = move
                Mindset = mindset
            }
        ] |> Set.ofList
    }
    let public roo(mindset) = {
        Advances = List.concat [
            Combinations.orthogonalOneTileMoves
        ] |> Set.ofList
        Captures = [
            for move in List.concat [
                Combinations.orthogonalOneTileMoves
            ] -> {
                Relocation = move
                Mindset = mindset
            }
        ] |> Set.ofList
    }
    let public bishop(mindset) = {
        Advances = List.concat [
            Combinations.diagonalOneTileMoves
            Combinations.diagonalTwoTileMoves
            Combinations.diagonalThreeTileMoves
        ] |> Set.ofList
        Captures = [
            for move in List.concat [
                Combinations.diagonalOneTileMoves
                Combinations.diagonalTwoTileMoves
                Combinations.diagonalThreeTileMoves
            ] -> {
                Relocation = move
                Mindset = mindset
            }
        ] |> Set.ofList
    }
    let public bishie(mindset) = {
        Advances = List.concat [
            Combinations.diagonalOneTileMoves
            Combinations.diagonalTwoTileMoves
        ] |> Set.ofList
        Captures = [
            for move in List.concat [
                Combinations.diagonalOneTileMoves
                Combinations.diagonalTwoTileMoves
            ] -> {
                Relocation = move
                Mindset = mindset
            }
        ] |> Set.ofList
    }
    let public bee(mindset) = {
        Advances = List.concat [
            Combinations.diagonalOneTileMoves
        ] |> Set.ofList
        Captures = [
            for move in List.concat [
                Combinations.diagonalOneTileMoves
            ] -> {
                Relocation = move
                Mindset = mindset
            }
        ] |> Set.ofList
    }
    let public youngMan = {
        Advances = List.concat [
            Combinations.orthogonalOneTileMoves
            Combinations.diagonalOneTileMoves
        ] |> Set.ofList
        Captures = Set.empty
    }
    let public man(mindset) = {
        Advances = List.concat [
            Combinations.orthogonalOneTileMoves
            Combinations.diagonalOneTileMoves
        ] |> Set.ofList
        Captures = [
            for move in List.concat [
                Combinations.orthogonalOneTileMoves
                Combinations.diagonalOneTileMoves
            ] -> {
                Relocation = move
                Mindset = mindset
            }
        ] |> Set.ofList
    }
    let public oldMan(mindset) = {
        Advances = Set.empty
        Captures = [
            for move in List.concat [
                Combinations.orthogonalOneTileMoves
                Combinations.diagonalOneTileMoves
            ] -> {
                Relocation = move
                Mindset = mindset
            }
        ] |> Set.ofList
    }
    let public youngHuman = {
        Advances = List.concat [
            Combinations.orthogonalOneTileMoves
            Combinations.orthogonalTwoTileMoves
            Combinations.diagonalOneTileMoves
            Combinations.diagonalTwoTileMoves
        ] |> Set.ofList
        Captures = Set.empty
    }
    let public human(mindset) = {
        Advances = List.concat [
            Combinations.orthogonalOneTileMoves
            Combinations.orthogonalTwoTileMoves
            Combinations.diagonalOneTileMoves
            Combinations.diagonalTwoTileMoves
        ] |> Set.ofList
        Captures = [
            for move in List.concat [
                Combinations.orthogonalOneTileMoves
                Combinations.orthogonalTwoTileMoves
                Combinations.diagonalOneTileMoves
                Combinations.diagonalTwoTileMoves
            ] -> {
                Relocation = move
                Mindset = mindset
            }
        ] |> Set.ofList
    }
    let public oldHuman(mindset) = {
        Advances = Set.empty
        Captures = [
            for move in List.concat [
                Combinations.orthogonalOneTileMoves
                Combinations.orthogonalTwoTileMoves
                Combinations.diagonalOneTileMoves
                Combinations.diagonalTwoTileMoves
            ] -> {
                Relocation = move
                Mindset = mindset
            }
        ] |> Set.ofList
    }
    let public youngWoman = {
        Advances = List.concat [
            Combinations.orthogonalOneTileMoves
            Combinations.orthogonalTwoTileMoves
            Combinations.orthogonalThreeTileMoves
            Combinations.diagonalOneTileMoves
            Combinations.diagonalTwoTileMoves
            Combinations.diagonalThreeTileMoves
        ] |> Set.ofList
        Captures = Set.empty
    }
    let public woman(mindset) = {
        Advances = List.concat [
            Combinations.orthogonalOneTileMoves
            Combinations.orthogonalTwoTileMoves
            Combinations.orthogonalThreeTileMoves
            Combinations.diagonalOneTileMoves
            Combinations.diagonalTwoTileMoves
            Combinations.diagonalThreeTileMoves
        ] |> Set.ofList
        Captures = [
            for move in List.concat [
                Combinations.orthogonalOneTileMoves
                Combinations.orthogonalTwoTileMoves
                Combinations.orthogonalThreeTileMoves
                Combinations.diagonalOneTileMoves
                Combinations.diagonalTwoTileMoves
                Combinations.diagonalThreeTileMoves
            ] -> {
                Relocation = move
                Mindset = mindset
            }
        ] |> Set.ofList
    }
    let public oldWoman(mindset) = {
        Advances = Set.empty
        Captures = [
            for move in List.concat [
                Combinations.orthogonalOneTileMoves
                Combinations.orthogonalTwoTileMoves
                Combinations.orthogonalThreeTileMoves
                Combinations.diagonalOneTileMoves
                Combinations.diagonalTwoTileMoves
                Combinations.diagonalThreeTileMoves
            ] -> {
                Relocation = move
                Mindset = mindset
            }
        ] |> Set.ofList
    }