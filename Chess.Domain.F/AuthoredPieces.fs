namespace Chess.Domain

module public NewPieces =
    let public sumpter = {
        Advances = Combinations.orthogonalKnightMoves |> Set.ofList
        Captures = Set.empty
    }
    let public destrier(fued) = {
        Advances = Set.empty
        Captures = [
            for move in Combinations.orthogonalKnightMoves -> {
                Relocation = move
                Fued       = fued
            }
        ] |> Set.ofList
    }
    let public knight(fued) = {
        Advances = Combinations.orthogonalKnightMoves |> Set.ofList
        Captures = [
            for move in Combinations.orthogonalKnightMoves -> {
                Relocation = move
                Fued       = fued
            }
        ] |> Set.ofList
    }
    let public palfrey = {
        Advances = Combinations.diagonalKnightMoves |> Set.ofList
        Captures = Set.empty
    }
    let public coursier(fued) = {
        Advances = Set.empty
        Captures = [
            for move in Combinations.diagonalKnightMoves -> {
                Relocation = move
                Fued       = fued
            }
        ] |> Set.ofList
    }
    let public paladin(fued) = {
        Advances = Combinations.diagonalKnightMoves |> Set.ofList
        Captures = [
            for move in Combinations.diagonalKnightMoves -> {
                Relocation = move
                Fued       = fued
            }
        ] |> Set.ofList
    }
    let public rook(fued) = {
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
                Fued       = fued
            }
        ] |> Set.ofList
    }
    let public rookie(fued) = {
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
                Fued       = fued
            }
        ] |> Set.ofList
    }
    let public roo(fued) = {
        Advances = List.concat [
            Combinations.orthogonalOneTileMoves
        ] |> Set.ofList
        Captures = [
            for move in List.concat [
                Combinations.orthogonalOneTileMoves
            ] -> {
                Relocation = move
                Fued       = fued
            }
        ] |> Set.ofList
    }
    let public bishop(fued) = {
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
                Fued       = fued
            }
        ] |> Set.ofList
    }
    let public bishie(fued) = {
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
                Fued       = fued
            }
        ] |> Set.ofList
    }
    let public bee(fued) = {
        Advances = List.concat [
            Combinations.diagonalOneTileMoves
        ] |> Set.ofList
        Captures = [
            for move in List.concat [
                Combinations.diagonalOneTileMoves
            ] -> {
                Relocation = move
                Fued       = fued
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
    let public man(fued) = {
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
                Fued       = fued
            }
        ] |> Set.ofList
    }
    let public oldMan(fued) = {
        Advances = Set.empty
        Captures = [
            for move in List.concat [
                Combinations.orthogonalOneTileMoves
                Combinations.diagonalOneTileMoves
            ] -> {
                Relocation = move
                Fued       = fued
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
    let public human(fued) = {
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
                Fued       = fued
            }
        ] |> Set.ofList
    }
    let public oldHuman(fued) = {
        Advances = Set.empty
        Captures = [
            for move in List.concat [
                Combinations.orthogonalOneTileMoves
                Combinations.orthogonalTwoTileMoves
                Combinations.diagonalOneTileMoves
                Combinations.diagonalTwoTileMoves
            ] -> {
                Relocation = move
                Fued       = fued
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
    let public woman(fued) = {
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
                Fued       = fued
            }
        ] |> Set.ofList
    }
    let public oldWoman(fued) = {
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
                Fued       = fued
            }
        ] |> Set.ofList
    }