# 🛒 Order Processing Console App

This is a C# console application that simulates a basic order processing system. The app allows you to create and manage orders through various statuses, based on real-life business rules and validations.

## 🧰 Technologies Used

- .NET / C#
- Console App
- Object-Oriented Programming (OOP)

## ✨ Features

- Create example orders or define them manually via console input
- Order structure includes:
  - Product name, unit price, quantity, total amount
  - Customer type (Company or Individual)
  - Customer name and full shipping address
  - Payment method (Card or Cash on Delivery)
- Business rules enforced:
  - Orders with **total ≥ 2500** and **Cash on Delivery** are returned to customer
  - Orders without a complete address result in an **Error**
  - Orders sent to shipping auto-close after 5 seconds
  - Closed orders **cannot be resent**

## 💡 How to Run

1. Clone the repository
   ```bash
    git clone https://github.com/aleTomasz/OrderProcessing.git

