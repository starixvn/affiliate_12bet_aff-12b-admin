

export class HeaderCell {
    constructor(key, text) {
        this.key = key;
        this.text = text;
    }

    get isVisable() {
        return true;
    }
}

export class SingleHeader extends HeaderCell {
    constructor(key, text, visable) {
        super(key, text);
        this.visable = visable;
    }

    get isVisable() {
        return this.visable;
    }
}

export class AggregateHeader extends HeaderCell {
    constructor(key, text, childHeader) {
        super(key, text);
        this.childerent = childHeader;
    }

    static getCols(headerAggregate) {
        let cells = [];
        headerAggregate.childerent.forEach(item => {
            if (item instanceof AggregateHeader) {
                cells.push(...AggregateHeader.getCols(item));
            }
            else {
                cells.push(item);
            }
        });
        return cells;
    }

    static convertColsToObject(cols) {
        let obj = {};
        cols.forEach(col => {
            if (col instanceof HeaderCell){
                obj[col.key] = col;
            }
        })
        return obj;
    }

    getCols = () =>  AggregateHeader.getCols(this);

    isVisableHeader = (key) => {
        let cols = this.getCols();
        let found = cols.find(item => item.key === key && item.isVisable);
        return !!found;
    }

    get isVisable() {
        let found = this.childerent.find(el => el.isVisable);
        return !!found;
    }

    get totalRow() {
        if (!this.isVisable) {
            return 0;
        }
        let intRow = 1;
        let maxChildRow = 1;
        this.childerent.forEach(item => {
            if (item instanceof AggregateHeader && item.isVisable) {
                let childRow = item.totalRow;
                maxChildRow = childRow > maxChildRow ? childRow : maxChildRow;
            }
        })
        return intRow + maxChildRow;
    }

    get totalVisableCol() {
        return this.childerent.filter(item  => item.isVisable).length;
    }
}

export class HeaderRow extends AggregateHeader {
    constructor(headerCols) {
        super("","", headerCols);
    }

    getHeaderCols = (depth) => {
        if (depth > this.totalRow - 1) {
            return [];
        }

        let currentDepth = 0;
        let currentCols = this.childerent;
        while (currentDepth < depth) {
            let nextDepthCols = [];
            currentCols.forEach(col => {
                if (col instanceof AggregateHeader) {
                    nextDepthCols.push(...col.childerent);
                }
            })
            currentCols = nextDepthCols;
            currentDepth++;
        }
        return currentCols;
    }

    getCol = (key) => {
        for(let i = 0; i++; i< this.totalRow)
        {
            let currentDepthCols = this.getHeaderCols(i);
            for(j = 0; j++; j< currentDepthCols.length)
            {
                if (currentDepthCols[j],key === key)
                {
                    return currentDepthCols[j];
                }    
            }
        }
    }

    get totalRow() {
        if (!this.isVisable)
        {
            return 0;
        }
        let totalRow = 0;
        this.childerent.forEach(item => {
            let childRow = item instanceof AggregateHeader ? item.totalRow : 1;
            totalRow = childRow > totalRow ? childRow : totalRow;
        })
        return totalRow;
    }
}