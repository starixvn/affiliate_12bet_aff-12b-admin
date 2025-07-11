import React from 'react'
import { Button } from 'reactstrap';
import { Cell } from '@9softstudio/react-fixed-table-header';
import { connect } from 'react-redux';
import { setParams, toggleSubmit } from '../../actions';
import { bindActionCreators } from 'redux';

const headerText = {
    No: '#',
    Month: 'Month',
    Affiliate: 'Affiliate',
    DealName: 'Deal Name',
    DealType: 'Deal Type',
    InstantPayment: 'Instant Payment',
    Quota: 'Quota',
    CpaAmount: 'CPA Amount',
    EstimatedCpaAmount: 'Estimated CPA Amount',
    ApprovedCpaAmount: 'Approved CPA Amount',
};

const headers = [
    {
        title: headerText.No,
        width: 10,
        sortable: false
    },
    {
        title: headerText.Month,
        width: 100,
        sortable: true,
        sortType: 1
    },
    {
        title: headerText.Affiliate,
        width: 120,
        sortable: true,
        sortType: 2
    },
    {
        title: headerText.DealName,
        width: 250,
        sortable: true,
        sortType: 3
    },
    {
        title: headerText.DealType,
        width: 150,
        sortable: true,
        sortType: 4 
    },
    {
        title: headerText.InstantPayment,
        width: 120,
        sortable: true,
        sortType: 5
    },
    {
        title: headerText.Quota,
        width: 100,
        sortable: true,
        sortType: 6
    },
    {
        title: headerText.CpaAmount,
        width: 120,
        sortable: true,
        sortType: 7
    },
    {
        title: headerText.EstimatedCpaAmount,
        width: 180,
        sortable: true,
        sortType: 8
    },
    {
        title: headerText.ApprovedCpaAmount,
        width: 180,
        sortable: true,
        sortType: 9
    }
]

export const TableHeader = ({ sortType, sortDirectionType, setParams, toggleSubmit }) => {
    const handleClickHeader = (sortTypeSelected) => {
        setParams({
            sortType: sortTypeSelected,
            sortDirectionType: sortTypeSelected === sortType ? !sortDirectionType : false
        });
        toggleSubmit();
    }

    return (
        <thead>
            <tr>
                {
                    headers.map((header) => (
                        <Cell header={true} colWidth={header.width} className="th">
                            <Button 
                                className="p-0 th-wrapper" 
                                color="link" 
                                onClick={() => handleClickHeader(header.sortType)}>
                                <span>{header.title}</span>
                                {
                                    header.sortable && (
                                        <div className="icon-container">
                                            <span className={`caret asc ${sortType === header.sortType && sortDirectionType ? "current" : ''}`}></span>
                                            <span className={`caret desc ${sortType === header.sortType && !sortDirectionType ? "current" : ''}`}></span>
                                        </div>
                                    )
                                }
                            </Button>
                        </Cell>
                    ))
                }
            </tr>
        </thead>
    )
}

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({ setParams, toggleSubmit }, dispatch);

}

export default connect(null, mapDispatchToProps)(TableHeader);
