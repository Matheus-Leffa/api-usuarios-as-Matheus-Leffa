using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public  class  Usuario

{
[Key]
public  int Id { get; set; }

[Required]
[MinLength(3)]
[MaxLength(100)]
public  string Nome { get; set; } = string.Empty;

[Required]
public  string Email { get; set; } = string.Empty;

[Required]
[MinLength(6)]
public  string Senha { get; set; } = string.Empty;

[Required]
public  DateTime DataNascimento { get; set; }

public  string Telefone { get; set; } = string.Empty;

[Required]
public  bool Ativo { get; set; } = true;

public  DateTime DataCriacao { get; set; } = DateTime.Now;

public  DateTime? DataAtualizacao { get; set; } = DateTime.Now;

}