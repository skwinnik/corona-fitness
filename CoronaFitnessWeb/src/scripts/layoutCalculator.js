class StyleRule {
    constructor(units, width, height, left, top) {
        this.display = 'block';
        this.position = 'absolute';
        this.width = width + units;
        this.height = height + units;
        this.left = left + units;
        this.top = top + units;
    }
}

class LayoutCalculator {
    constructor(elementIds, containerWidth, containerHeight, units) {
        let matrix = LayoutCalculator.getMatrix(elementIds.length);
        this.elementIds = elementIds;
        this.units = units;
        this.cols = matrix.cols;
        this.rows = matrix.rows;

        this.maxWidth = containerWidth;
        this.maxHeight = containerHeight;
    }

    static getMatrix(n) {
        let root = Math.sqrt(n);
        return {cols: Math.ceil(root), rows: Math.round(root)}
    }

    calcWidth() {
        return Math.floor(this.maxWidth / this.cols);
    }

    calcHeight() {
        return Math.floor(this.maxHeight / this.rows);
    }

    calcLeft(n, width) {
        return (n % this.cols) * width;
    }

    calcTop(n, height) {
        return Math.floor(n / this.cols) * height;
    }

    getStyles() {
        const width = this.calcWidth();
        const height = this.calcHeight();

        return this.elementIds
            .reduce((map, id, inx) => {
                map[id] = new StyleRule(this.units, width, height, this.calcLeft(inx, width), this.calcTop(inx, height));
                return map;
            }, {});
    }
}

class LayoutCalculatorPercentage extends LayoutCalculator {
    constructor(elementIds) {
        super(elementIds, 100, 100, '%');
    }
}

export {
    LayoutCalculator, LayoutCalculatorPercentage
}

//todo tests