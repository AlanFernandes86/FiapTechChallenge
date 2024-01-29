USE [TechChallenge]
GO
/****** Object:  Table [dbo].[client]    Script Date: 1/28/2024 20:15:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[client](
	[cpf] [varchar](11) NOT NULL,
	[name] [varchar](100) NULL,
	[email] [varchar](100) NULL,
	[updated_at] [datetime] NOT NULL,
	[created_at] [datetime] NOT NULL,
 CONSTRAINT [PK_client] PRIMARY KEY CLUSTERED 
(
	[cpf] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order]    Script Date: 1/28/2024 20:15:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[order_status_id] [int] NOT NULL,
	[client_cpf] [varchar](11) NULL,
	[prepare_in_minutes] [int] NULL,
	[updated_at] [datetime] NOT NULL,
	[created_at] [datetime] NOT NULL,
 CONSTRAINT [PK_order] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order_product]    Script Date: 1/28/2024 20:15:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_product](
	[id] [int] NOT NULL,
	[product_id] [int] NOT NULL,
	[order_id] [int] NOT NULL,
	[price] [float] NOT NULL,
	[updated_at] [datetime] NOT NULL,
	[created_at] [datetime] NOT NULL,
 CONSTRAINT [PK_order_product] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order_status]    Script Date: 1/28/2024 20:15:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_status](
	[id] [int] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[updated_at] [datetime] NOT NULL,
	[created_at] [datetime] NOT NULL,
 CONSTRAINT [PK_order_status] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[payment]    Script Date: 1/28/2024 20:15:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[payment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[order_id] [int] NOT NULL,
	[value] [float] NOT NULL,
	[method] [int] NULL,
	[updated_at] [datetime] NOT NULL,
	[created_at] [datetime] NOT NULL,
 CONSTRAINT [PK_payment] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product]    Script Date: 1/28/2024 20:15:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[product_category_id] [int] NOT NULL,
	[name] [varchar](100) NOT NULL,
	[description] [varchar](500) NOT NULL,
	[price] [float] NOT NULL,
	[updated_at] [datetime] NOT NULL,
	[created_at] [datetime] NOT NULL,
 CONSTRAINT [PK_product_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product_category]    Script Date: 1/28/2024 20:15:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_category](
	[id] [int] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[updated_at] [datetime] NOT NULL,
	[created_at] [datetime] NOT NULL,
 CONSTRAINT [PK_product_category_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[order]  WITH CHECK ADD  CONSTRAINT [FK_order_client] FOREIGN KEY([client_cpf])
REFERENCES [dbo].[client] ([cpf])
GO
ALTER TABLE [dbo].[order] CHECK CONSTRAINT [FK_order_client]
GO
ALTER TABLE [dbo].[order]  WITH CHECK ADD  CONSTRAINT [FK_order_order_status] FOREIGN KEY([order_status_id])
REFERENCES [dbo].[order_status] ([id])
GO
ALTER TABLE [dbo].[order] CHECK CONSTRAINT [FK_order_order_status]
GO
ALTER TABLE [dbo].[order_product]  WITH CHECK ADD  CONSTRAINT [FK_order_product_order] FOREIGN KEY([order_id])
REFERENCES [dbo].[order] ([id])
GO
ALTER TABLE [dbo].[order_product] CHECK CONSTRAINT [FK_order_product_order]
GO
ALTER TABLE [dbo].[order_product]  WITH CHECK ADD  CONSTRAINT [FK_order_product_product] FOREIGN KEY([product_id])
REFERENCES [dbo].[product] ([id])
GO
ALTER TABLE [dbo].[order_product] CHECK CONSTRAINT [FK_order_product_product]
GO
ALTER TABLE [dbo].[payment]  WITH CHECK ADD  CONSTRAINT [FK_payment_order] FOREIGN KEY([order_id])
REFERENCES [dbo].[order] ([id])
GO
ALTER TABLE [dbo].[payment] CHECK CONSTRAINT [FK_payment_order]
GO
ALTER TABLE [dbo].[product]  WITH CHECK ADD  CONSTRAINT [FK_product_product_category] FOREIGN KEY([product_category_id])
REFERENCES [dbo].[product_category] ([id])
GO
ALTER TABLE [dbo].[product] CHECK CONSTRAINT [FK_product_product_category]
GO
USE [master]
GO
ALTER DATABASE [TechChallenge] SET  READ_WRITE 
GO