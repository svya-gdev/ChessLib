namespace Chess.Domain

module public NewPieces =
    let public sumpter = {
        Advances = Combinations.orthogonalKnightMoves |> Set.ofList
        Captures = Set.empty
    }
    let public destrier(feud) = {
        Advances = Set.empty
        Captures = [
            for move in Combinations.orthogonalKnightMoves -> {
                Relocation = move
                Feud       = feud
            }
        ] |> Set.ofList
    }
    let public knight(feud) = {
        Advances = Combinations.orthogonalKnightMoves |> Set.ofList
        Captures = [
            for move in Combinations.orthogonalKnightMoves -> {
                Relocation = move
                Feud       = feud
            }
        ] |> Set.ofList
    }
    let public palfrey = {
        Advances = Combinations.diagonalKnightMoves |> Set.ofList
        Captures = Set.empty
    }
    let public coursier(feud) = {
        Advances = Set.empty
        Captures = [
            for move in Combinations.diagonalKnightMoves -> {
                Relocation = move
                Feud       = feud
            }
        ] |> Set.ofList
    }
    let public paladin(feud) = {
        Advances = Combinations.diagonalKnightMoves |> Set.ofList
        Captures = [
            for move in Combinations.diagonalKnightMoves -> {
                Relocation = move
                Feud       = feud
            }
        ] |> Set.ofList
    }
    let public rook(feud) = {
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
                Feud       = feud
            }
        ] |> Set.ofList
    }
    let public rookie(feud) = {
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
                Feud       = feud
            }
        ] |> Set.ofList
    }
    let public roo(feud) = {
        Advances = List.concat [
            Combinations.orthogonalOneTileMoves
        ] |> Set.ofList
        Captures = [
            for move in List.concat [
                Combinations.orthogonalOneTileMoves
            ] -> {
                Relocation = move
                Feud       = feud
            }
        ] |> Set.ofList
    }
    let public bishop(feud) = {
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
                Feud       = feud
            }
        ] |> Set.ofList
    }
    let public bishie(feud) = {
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
                Feud       = feud
            }
        ] |> Set.ofList
    }
    let public bee(feud) = {
        Advances = List.concat [
            Combinations.diagonalOneTileMoves
        ] |> Set.ofList
        Captures = [
            for move in List.concat [
                Combinations.diagonalOneTileMoves
            ] -> {
                Relocation = move
                Feud       = feud
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
    let public man(feud) = {
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
                Feud       = feud
            }
        ] |> Set.ofList
    }
    let public oldMan(feud) = {
        Advances = Set.empty
        Captures = [
            for move in List.concat [
                Combinations.orthogonalOneTileMoves
                Combinations.diagonalOneTileMoves
            ] -> {
                Relocation = move
                Feud       = feud
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
    let public human(feud) = {
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
                Feud       = feud
            }
        ] |> Set.ofList
    }
    let public oldHuman(feud) = {
        Advances = Set.empty
        Captures = [
            for move in List.concat [
                Combinations.orthogonalOneTileMoves
                Combinations.orthogonalTwoTileMoves
                Combinations.diagonalOneTileMoves
                Combinations.diagonalTwoTileMoves
            ] -> {
                Relocation = move
                Feud       = feud
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
    let public woman(feud) = {
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
                Feud       = feud
            }
        ] |> Set.ofList
    }
    let public oldWoman(feud) = {
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
                Feud       = feud
            }
        ] |> Set.ofList
    }