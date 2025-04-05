CREATE TABLE categories(
	id INT PRIMARY KEY IDENTITY(1,1),
	category VARCHAR(MAX) NULL,
	type VARCHAR(MAX) NULL,
	status VARCHAR(MAX) NULL,
	date_insert Date NULL
)

SELECT * FROM categories

CREATE TABLE income(
	id INT PRIMARY KEY IDENTITY(1,1),
	category VARCHAR(MAX) NULL,
	item VARCHAR(MAX) NULL,
	income FLOAT NULL,
	description VARCHAR(MAX) NULL,
	date_income Date NULL,
	date_insert Date NULL
)

SELECT * FROM income

CREATE TABLE expenses(
	id INT PRIMARY KEY IDENTITY(1,1),
	category VARCHAR(MAX) NULL,
	item VARCHAR(MAX) NULL,
	income FLOAT NULL,
	description VARCHAR(MAX) NULL,
	date_expense Date NULL,
	date_insert Date NULL
)

EXEC sp_rename 'expenses.income', 'cost', 'COLUMN';

CREATE TABLE expenses(
	id INT PRIMARY KEY IDENTITY(1,1),
	category VARCHAR(MAX) NULL,
	item VARCHAR(MAX) NULL,
	cost FLOAT NULL,
	description VARCHAR(MAX) NULL,
	date_expense DATE NULL,
	date_insert DATE NULL
);

SELECT * FROM expenses