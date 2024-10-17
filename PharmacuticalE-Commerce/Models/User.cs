using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace PharmacuticalE_Commerce.Models;

public partial class User : IdentityUser
{
    public string? Fname { get; set; } = null!;

    public string? Lname { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<ShippingAddress> ShippingAddresses { get; set; } = new List<ShippingAddress>();

    public virtual ICollection<UserCard> UserCards { get; set; } = new List<UserCard>();
}
