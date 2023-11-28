"use strict";

class Chart {

    #t;
    #subIntervals;
    #n;
    #numberOfTrajectories;
    #coordinates;
    #values;
    #YFunc;
    #trajectoriesColor;
    #maxValue;
    #minValue;

    get values(){
        return this.#values;
    }

    get coordinates(){
        return this.#coordinates;
    }

    get trajectoriesColor(){
        return this.#trajectoriesColor;
    }

    get n(){
        return this.#n;
    }

    get maxValue(){
        return this.#maxValue;
    }

    get minValue(){
        return this.#minValue;
    }

    constructor(T, numberOfTrajectories, N, yFunc) {
        this.#t = T;
        this.#subIntervals = N;
        this.#n = this.#t * this.#subIntervals;
        this.#numberOfTrajectories = numberOfTrajectories;
        this.#coordinates = [];
        
        this.#values = Array(this.#numberOfTrajectories).fill().map(() => Array(this.#n).fill(0));
        
        this.#YFunc = yFunc;
        this.#trajectoriesColor = Array(this.#numberOfTrajectories).fill(0);

        this.#maxValue = Number.MIN_VALUE;
        this.#minValue = Number.MAX_VALUE;
        
        this.simulate();

    }

    simulate() {
        for (let k = 0; k < this.#numberOfTrajectories; k++) { 

            const points = Array(this.#n);
            points[0] = new Point(0, 0);

            for (let i = 1; i < this.#n; i++) {
                this.#values[k][i] = this.#YFunc(this.#values, k, i);
                points[i] = new Point(i, this.#values[k][i]);
    
                if (this.#values[k][i] > this.#maxValue) {
                    this.#maxValue = this.#values[k][i];
                } else if (this.#values[k][i] < this.#minValue) {
                    this.#minValue = this.#values[k][i];
                }
            }
            this.#coordinates.push(points);
            this.#trajectoriesColor[k] = `rgb(${Math.floor(Math.random() * 256)}, ${Math.floor(Math.random() * 256)}, ${Math.floor(Math.random() * 256)})`;
        }
    }    

    
}
