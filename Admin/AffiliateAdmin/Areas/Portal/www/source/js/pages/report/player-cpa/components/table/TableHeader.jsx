import React from 'react'
import { Button } from 'reactstrap';
import { Cell } from '@9softstudio/react-fixed-table-header';
import { connect } from 'react-redux';
import { setParams, toggleSubmit } from '../../actions';
import { bindActionCreators } from 'redux';

const headerText = {
    No: '#',
    Affiliate: 'Affiliate',
    Player: 'Player',
    RegistrationDate: 'Registration Date',
    DealName: 'Deal Name',
    PaymentFrequency: 'Payment Frequency',
    FTD: 'FTD',
    TotalDeposit: 'Total Deposit',
    Turnover: 'Turnover',
    ActiveQualifiedDate: 'Active Qualified Date',
    CPAAmount: 'CPA Amount',
    BetTicket: 'Bet Ticket',
    HoldingQualifiedDate: 'Holding Qualified Date',
    HoldingExpiredDate: 'Holding Expired Date',
    EstimatedCPAAmount: 'Estimated CPA Amount',
    ApprovedCPAAmount: 'Approved CPA Amount',
    CPAStatus: 'CPA Status',
    ApprovalDate: 'Approval Date'
};

const headers = [
    {
        title: headerText.No,
        width: 40,
        sortable: false
    },
    {
        title: headerText.Affiliate,
        width: 100,
        sortable: true,
        sortType: 1
    },
    {
        title: headerText.Player,
        width: 100,
        sortable: true,
        sortType: 2
    },
    {
        title: headerText.RegistrationDate,
        width: 120,
        sortable: true,
        sortType: 3
    },
    {
        title: headerText.DealName,
        width: 150,
        sortable: true,
        sortType: 4
    },
    {
        title: headerText.PaymentFrequency,
        width: 120,
        sortable: true,
        sortType: 5
    },
    {
        title: headerText.FTD,
        width: 80,
        sortable: true,
        sortType: 6
    },
    {
        title: headerText.TotalDeposit,
        width: 100,
        sortable: true,
        sortType: 7
    },
    {
        title: headerText.Turnover,
        width: 100,
        sortable: true,
        sortType: 8
    },
    {
        title: headerText.ActiveQualifiedDate,
        width: 120,
        sortable: true,
        sortType: 9
    },
    {
        title: headerText.CPAAmount,
        width: 100,
        sortable: true,
        sortType: 10
    },
    {
        title: headerText.BetTicket,
        width: 80,
        sortable: true,
        sortType: 11
    },
    {
        title: headerText.HoldingQualifiedDate,
        width: 120,
        sortable: true,
        sortType: 12
    },
    {
        title: headerText.HoldingExpiredDate,
        width: 120,
        sortable: true,
        sortType: 13
    },
    {
        title: headerText.EstimatedCPAAmount,
        width: 120,
        sortable: true,
        sortType: 14
    },
    {
        title: headerText.ApprovedCPAAmount,
        width: 120,
        sortable: true,
        sortType: 15
    },
    {
        title: headerText.CPAStatus,
        width: 100,
        sortable: true,
        sortType: 16
    },
    {
        title: headerText.ApprovalDate,
        width: 120,
        sortable: true,
        sortType: 17
    }
];

export const TableHeader = ({ sortType, sortDirectionType, setParams, toggleSubmit }) => {
    const handleClickHeader = (sortTypeSelected) => {
        if (!sortTypeSelected) return;
        
        setParams({
            sortType: sortTypeSelected,
            sortDirectionType: sortTypeSelected === sortType ? !sortDirectionType : false
        });
        toggleSubmit();
    }

    return (
        <thead>
            <tr>
                {headers.map((header, index) => (
                    <Cell key={index} header={true} colWidth={header.width} className="th">
                        <Button 
                            className="p-0 th-wrapper" 
                            color="link" 
                            onClick={() => handleClickHeader(header.sortType)}>
                            <span>{header.title}</span>
                            {header.sortable && (
                                <div className="icon-container">
                                    <span className={`caret asc ${sortType === header.sortType && sortDirectionType ? "current" : ''}`}></span>
                                    <span className={`caret desc ${sortType === header.sortType && !sortDirectionType ? "current" : ''}`}></span>
                                </div>
                            )}
                        </Button>
                    </Cell>
                ))}
            </tr>
        </thead>
    )
}

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({ setParams, toggleSubmit }, dispatch);
}

export default connect(null, mapDispatchToProps)(TableHeader);
