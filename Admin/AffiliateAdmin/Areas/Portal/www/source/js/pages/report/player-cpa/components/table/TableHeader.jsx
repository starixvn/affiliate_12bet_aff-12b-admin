import React from 'react'
import { Button } from 'reactstrap';
import { Cell } from '@9softstudio/react-fixed-table-header';
import { connect } from 'react-redux';
import { setParams, toggleSubmit } from '../../actions';
import { bindActionCreators } from 'redux';
import { headerRows, dataKey } from '../../libs/constants';

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
                            {cell.sortable ? (
                                <Button
                                    className="th-wrapper"
                                    color="link"
                                    onClick={() => handleClickHeader(cell.sortColumn)}>
                                    {cell.title}
                                    {
                                        cell.field === dataKey.CPAAmount && (
                                            <span className="oi icon-info ml-1" title='Click to view Turnover CPA Detail'></span>
                                        )
                                    }
                                    <div className="icon-container">
                                        <span className={`caret asc ${sortColumn === cell.sortColumn && isAscending ? "current" : ''}`}></span>
                                        <span className={`caret desc ${sortColumn === cell.sortColumn && !isAscending ? "current" : ''}`}></span>
                                    </div>
                                </Button>
                            ) :
                            (
                                <div className="th-wrapper">
                                    {cell.title}
                                </div>
                            )}
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
