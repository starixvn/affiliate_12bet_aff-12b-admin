import React from 'react';
import { GridData } from '../GridData';
import { shallow } from 'enzyme';
jest.mock('../../../modules/constant');

describe('commission group - GridData ', () => {
    const tierSettingItem = {
        Commission: 0.02,
        GroupId: 153,
        MaxAmount: 100000000000000,
        MaxQm: 1000000000,
        MinAmount: 0,
        MinQm: 0,
        TierId: 1
    };
    const dataItem = {
        BankingFee: {
            DeductedPercentage: 0.02,
            DeductionId: 12,
            DeductionName: "Banking Fee",
            GroupId: 153,
            NextDeductedPercentage: 0.02,
            ProductIds: "0"
        },
        CreatedDate: "/Date(1534273172607)/",
        CreatedDateText: "08/15/2018 01:59:32",
        Creator: "AAnina",
        Description: "",
        GroupId: 153,
        GroupName: "CG22125",
        NextQmTurnover: 100,
        NoAffs: 2,
        OperationalFee: {
            DeductedPercentage: 0.15,
            DeductionId: 11,
            DeductionName: "Operational Fee",
            GroupId: 153,
            NextDeductedPercentage: 0.15,
            ProductIds: "0"
        },
        PlatformFeeList: [
            {
                DeductedPercentage: 0.17,
                DeductionId: 10,
                DeductionName: "Platform Fee",
                GroupId: 153,
                NextDeductedPercentage: 0.17,
                ProductIds: "1,3,4,5,6,7,8,13,14,15,16,17,18"
            }
        ],
        QmTurnover: 100,
        RowIdx: 6,
        TierSettingList: [
            tierSettingItem
        ]
    };
    const defaultProps = {
        productList: [
            { ProductId: 1, ProductName: "Sportsbook" },
            { ProductId: 3, ProductName: "Playtech Casino" },
            { ProductId: 4, ProductName: "Number Game" },
            { ProductId: 5, ProductName: "Virtual Sports" },
            { ProductId: 6, ProductName: "Keno" }
        ],
        gridData: [
            dataItem
        ]
    };

    it('_convertToProductNameText - return correct data with correct parameter', () => {
        const productIdList = "1,3,4,5,6,7,8,13,14,15,16,17,18";
        const expectResult = "Sportsbook,Playtech Casino,Number Game,Virtual Sports,Keno";
        const wrapper = shallow(<GridData {...defaultProps} />);

        const result = wrapper.instance()._convertToProductNameText(productIdList);

        expect(result).toEqual(expectResult);
    });

    it('handleDelete - delete Commission Group Successfully', async () => {
        const expectedResponse = { data: { IsSuccessful: true, Message: "OK" }, status: 200 };
        const mockDeleteCommGroup = jest.fn(() => expectedResponse);
        const mockGetGroupSettingList = jest.fn();

        const props = Object.assign({}, defaultProps, { deleteCommGroup: mockDeleteCommGroup, getGroupSettingList: mockGetGroupSettingList });
        const componentInstance = new GridData(props);
        window.confirm = jest.fn(() => true);
        window.alert = jest.fn();

        await componentInstance.handleDelete(153, "CG22125");

        expect(window.confirm).toHaveBeenCalledWith("Are you sure you want to delete the commission group CG22125 ?");
        expect(props.deleteCommGroup).toHaveBeenCalledWith({ "groupId": 153 });
        expect(props.getGroupSettingList).toHaveBeenCalledWith({ PageIndex: 1, PageList: [], PageSize: 10, TotalItem: 0 });
        expect(window.alert).toHaveBeenCalledWith("Delete Commission Group Successfully");
    });

    it('handleDelete - delete Commission Group Failed', async () => {
        const expectedResponse = { data: { IsSuccessful: false, Message: "Failed" }, status: 200 };
        const mockDeleteCommGroup = jest.fn(() => expectedResponse);
        const mockGetGroupSettingList = jest.fn();

        const props = Object.assign({}, defaultProps, { deleteCommGroup: mockDeleteCommGroup, getGroupSettingList: mockGetGroupSettingList });
        const componentInstance = new GridData(props);
        window.confirm = jest.fn(() => true);
        window.alert = jest.fn();

        await componentInstance.handleDelete(153, "CG22125");

        expect(window.confirm).toHaveBeenCalledWith("Are you sure you want to delete the commission group CG22125 ?");
        expect(props.deleteCommGroup).toHaveBeenCalledWith({ "groupId": 153 });
        expect(props.getGroupSettingList).not.toHaveBeenCalled();
        expect(window.alert).toHaveBeenCalledWith("Failed");
    });

    it('handleDelete - reponse is incorrect', async () => {
        const expectedResponse = { data: { IsSuccessful: false, Message: "Failed" }, status: 500 };
        const mockDeleteCommGroup = jest.fn(() => expectedResponse);

        const props = Object.assign({}, defaultProps, { deleteCommGroup: mockDeleteCommGroup });
        const componentInstance = new GridData(props);
        window.confirm = jest.fn(() => true);
        window.alert = jest.fn();

        await componentInstance.handleDelete(153, "CG22125");

        expect(window.confirm).toHaveBeenCalledWith("Are you sure you want to delete the commission group CG22125 ?");
        expect(props.deleteCommGroup).toHaveBeenCalledWith({ "groupId": 153 });
        expect(window.alert).not.toHaveBeenCalled();
    });

    it('handleDelete - comfirm is cancel', async () => {
        const expectedResponse = { data: { IsSuccessful: false, Message: "Failed" }, status: 500 };
        const mockDeleteCommGroup = jest.fn(() => expectedResponse);

        const props = Object.assign({}, defaultProps, { deleteCommGroup: mockDeleteCommGroup });
        const componentInstance = new GridData(props);
        window.confirm = jest.fn(() => false);

        await componentInstance.handleDelete(153, "CG22125");

        expect(window.confirm).toHaveBeenCalledWith("Are you sure you want to delete the commission group CG22125 ?");
        expect(props.deleteCommGroup).not.toHaveBeenCalled();
    });

    it('_renderHeader - render correctly', () => {
        const componentInstance = new GridData(defaultProps);

        const renderHeader = shallow(<div>{componentInstance._renderHeader()}</div>);

        expect(renderHeader).toMatchSnapshot();
    });

    it('_renderGroupData - render correctly', () => {
        const componentInstance = new GridData(defaultProps);

        const renderGroupData = shallow(<div>{componentInstance._renderGroupData()}</div>);

        expect(renderGroupData).toMatchSnapshot();
    });

    it('_renderGroupData - render correctly with groupId > 1000, MaxAmount and MaxQm is not max value', () => {
        const tierSetting = Object.assign({}, tierSettingItem, { MaxAmount: 100, MaxQm: 10 });
        const data = Object.assign({}, dataItem, { GroupId: 927841, TierSettingList: [tierSetting] })
        const props = Object.assign({}, defaultProps, { gridData: [data] });
        const componentInstance = new GridData(props);

        const renderGroupData = shallow(<div>{componentInstance._renderGroupData()}</div>);

        expect(renderGroupData).toMatchSnapshot();
    });

    it('_renderGroupData - should call onEdit when click button edit', () => {
        const props = Object.assign({}, defaultProps, { onEdit: jest.fn() })
        const componentInstance = new GridData(props);

        const renderGroupData = shallow(<div>{componentInstance._renderGroupData()}</div>);

        renderGroupData.find('button').at(0).simulate('click')

        expect(props.onEdit).toHaveBeenCalledWith(153);
    });

    it('_renderGroupData - should call handleDelete when click button edit', () => {
        const data = Object.assign({}, dataItem, { NoAffs: 0 })
        const props = Object.assign({}, defaultProps, { gridData: [data] });
        const componentInstance = new GridData(props);
        componentInstance.handleDelete = jest.fn();
        const renderGroupData = shallow(<div>{componentInstance._renderGroupData()}</div>);

        renderGroupData.find('button').at(1).simulate('click')

        expect(componentInstance.handleDelete).toHaveBeenCalledWith(153, "CG22125");
    });

    it('Should render correctly', () => {
        const wrapper = shallow(<GridData {...defaultProps} />);

        expect(wrapper).toMatchSnapshot();
    });

    it('Should render correctly with is private group', () => {
        const dataPrivateGroupItem = {
            BankingFee: {
                DeductedPercentage: 0.02,
                DeductionId: 12,
                DeductionName: "Banking Fee",
                GroupId: 1271808,
                NextDeductedPercentage: 0.02,
                ProductIds: "0"
            },
            CreatedDate: "/Date(1534273172607)/",
            CreatedDateText: "08/15/2018 01:59:32",
            Creator: "AAnina",
            Description: "",
            GroupId: 1271808,
            GroupName: "12BETAFF226",
            NextQmTurnover: 100,
            NoAffs: 2,
            OperationalFee: {
                DeductedPercentage: 0.15,
                DeductionId: 11,
                DeductionName: "Operational Fee",
                GroupId: 1271808,
                NextDeductedPercentage: 0.15,
                ProductIds: "0"
            },
            PlatformFeeList: [
                {
                    DeductedPercentage: 0.17,
                    DeductionId: 10,
                    DeductionName: "Platform Fee",
                    GroupId: 1271808,
                    NextDeductedPercentage: 0.17,
                    ProductIds: "1,3,4,5,6,7,8,13,14,15,16,17,18"
                }
            ],
            QmTurnover: 100,
            RowIdx: 6,
            TierSettingList: [
                tierSettingItem
            ]
        };
        const props = Object.assign({}, defaultProps, { gridData: [dataPrivateGroupItem] });
        const wrapper = shallow(<GridData {...props} />);

        expect(wrapper).toMatchSnapshot();
    });

    it('Should render correctly with data is null', () => {
        const props = Object.assign({}, defaultProps, { gridData: [] });
        const wrapper = shallow(<GridData {...props} />);

        expect(wrapper).toMatchSnapshot();
    });
});