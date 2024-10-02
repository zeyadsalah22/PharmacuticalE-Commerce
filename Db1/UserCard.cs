﻿using System;
using System.Collections.Generic;

namespace Db1;

public partial class UserCard
{
    public int CardId { get; set; }

    public string CardNo { get; set; } = null!;

    public int UserId { get; set; }

    public string HolderName { get; set; } = null!;

    public DateTime ExpirationDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual User User { get; set; } = null!;
}