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

const row1 = [
    {
        title: headerText.No,
        width: 60,
        sortable: false,
        rowSpan: 2
    },
    {
        title: headerText.Affiliate,
        width: 150,
        sortable: true,
        sortColumn: "AffUserName",
        rowSpan: 2
    },
    {
        title: headerText.Player,
        width: 150,
        sortable: true,
        sortColumn: "CustUserName",
        rowSpan: 2
    },
    {
        title: headerText.RegistrationDate,
        width: 150,
        sortable: true,
        sortColumn: "RegisteredDate",
        rowSpan: 2
    },
    {
        title: headerText.DealName,
        width: 150,
        sortable: true,
        sortColumn: "DealName",
        rowSpan: 2
    },
    {
        title: headerText.PaymentFrequency,
        width: 120,
        sortable: true,
        sortColumn: "ScheduleName",
        rowSpan: 2
    },
    {
        title: "Active Period",
        sortable: false,
        columnSpan: 5,
        width: 150 * 5
    },
    {
        title: "Holding Period",
        sortable: false,
        columnSpan: 4,
        width: 150 * 4
    },
    {
        title: "Approval Period",
        sortable: false,
        columnSpan: 3,
        width: 150 * 3
    }
];

const row2 =[
    {
        title: headerText.FTD,
        width: 100,
        sortable: true,
        sortColumn: "FTD"
    },
    {
        title: headerText.TotalDeposit,
        width: 150,
        sortable: true,
        sortColumn: "TotalDeposit"
    },
    {
        title: headerText.Turnover,
        width: 150,
        sortable: true,
        sortColumn: "IsTO"
    },
    {
        title: headerText.ActiveQualifiedDate,
        width: 150,
        sortable: true,
        sortColumn: "AQD"
    },
    {
        title: headerText.CPAAmount,
        width: 150,
        sortable: true,
        sortColumn: "CPAAmount"
    },
    {
        title: headerText.BetTicket,
        width: 150,
        sortable: true,
        sortColumn: "HoldingBetTickets"
    },
    {
        title: headerText.HoldingQualifiedDate,
        width: 150,
        sortable: true,
        sortColumn: "HQD"
    },
    {
        title: headerText.HoldingExpiredDate,
        width: 150,
        sortable: true,
        sortColumn: "HED"
    },
    {
        title: headerText.EstimatedCPAAmount,
        width: 150,
        sortable: true,
        sortColumn: "EstimatedCPAAmount"
    },
    {
        title: headerText.ApprovedCPAAmount,
        width: 150,
        sortable: true,
        sortColumn: "ApprovedCPAAmount"
    },
    {
        title: headerText.CPAStatus,
        width: 150,
        sortable: true,
        sortColumn: "CPAStatus"
    },
    {
        title: headerText.ApprovalDate,
        width: 150,
        sortable: true,
        sortColumn: "CPADate"
    }
]

const headerRows = [row1, row2]

const RowHeader = ({ index, row, sortColumn, isAscending, handleClickHeader }) => {
    return (
        <tr className={`freeze-row-h${index}`}>
            {
            row.map((cell, index) => {
            return (
                <Cell 
                    key={index} 
                    header={true} 
                    style={{ width: cell.width, maxWidth: cell.width }}
                    className={`freeze-column-${index}`} 
                    rowSpan={cell.rowSpan} 
                    colSpan={cell.columnSpan}>
                    <Button 
                        className="p-0 th-wrapper" 
                        color="link" 
                        onClick={() => handleClickHeader(cell.sortColumn)}>
                        <span>{cell.title}</span>
                        {cell.sortable && (
                            <div className="icon-container">
                                <span className={`caret asc ${sortColumn === cell.sortColumn && isAscending ? "current" : ''}`}></span>
                                <span className={`caret desc ${sortColumn === cell.sortColumn && !isAscending ? "current" : ''}`}></span>
                            </div>
                        )}
                    </Button>
                </Cell>
                )
            })
            }
        </tr>
    )
}

export const TableHeader = ({ sortColumn, isAscending, setParams, toggleSubmit }) => {
    const handleClickHeader = (sortColumnSelected) => {
        if (!sortColumnSelected) return;
        
        setParams({
            sortColumn: sortColumnSelected,
            isAscending: sortColumnSelected === sortColumn ? !isAscending : false
        });
        toggleSubmit();
    }

    return (
        <thead>
            {
            headerRows.map((row, index) => {
                return (
                    <RowHeader 
                        key={index} 
                        index={index + 1}
                        row={row} 
                        handleClickHeader={handleClickHeader} 
                        sortColumn={sortColumn} 
                        isAscending={isAscending} />
                )
            })
            }
            
        </thead>
    )
}

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({ setParams, toggleSubmit }, dispatch);
}

export default connect(null, mapDispatchToProps)(TableHeader);
