﻿using BitCoinChallange.Domain.Kernel.Queries;
using BitCoinChallange.Domain.Validations;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;

namespace BitCoinChallange.Domain.Queries
{
	public class BookQueryRequest : Query, IRequest<IEnumerable<BookQueryResponse>>
	{
		public BookQueryRequest(string name, string ordering, BookSpecificationsRequest specifications)
		{
			AggregateId = Guid.NewGuid();
			Name = name;
			Ordering = ordering?.ToUpper();
			Specifications = specifications ?? new BookSpecificationsRequest(string.Empty, string.Empty, 0, new List<string>(), new List<string>());
		}

		public string Name { get; private set; }
		public string Ordering { get; private set; }
		public BookSpecificationsRequest Specifications { get; private set; }

		public override bool IsValid()
		{
			ValidationResult = new BookQueryValidation().Validate(this);
			return ValidationResult.IsValid;
		}

		public static class Factory
		{
			public static BookQueryRequest Create(string name, string ordering, BookSpecificationsRequest specifications)
			{
				return new BookQueryRequest(name, ordering, specifications);
			}
		}
	}
}
