Select p.Name as 'ProductName', c.Name as 'CategoryName'
from Products p
         left join ProductCategoryIds pc on p.Id == pc.ProductIds
         left Join Categories c on pc.CategoryId == c.Id