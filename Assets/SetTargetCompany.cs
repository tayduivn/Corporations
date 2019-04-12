﻿public enum TargetCompany
{
    Selected,
    Product,
    Group
}

public class SetTargetCompany : View
{
    public TargetCompany TargetCompany;
    public int companyId;

    void UpdateTargetCompany()
    {
        switch (TargetCompany)
        {
            case TargetCompany.Group:
                companyId = MyGroupEntity.company.Id;
                break;

            case TargetCompany.Product:
                companyId = MyProductEntity.company.Id;
                break;

            case TargetCompany.Selected:
                companyId = SelectedCompany.company.Id;
                break;
        }
    }

    void OnEnable()
    {
        UpdateTargetCompany();
    }

    void Update()
    {
        UpdateTargetCompany();
    }
}
