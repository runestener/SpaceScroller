using UnityEngine;
using System.Collections;

public class EnemyShip : SpaceShip {

	public void SetupEnemyShipData(EnemyData enemyData) {
		this.healthPoints = enemyData.healthPoints;
		this.spriteRenderer.sprite = enemyData.sprite;
		this.killPoints = enemyData.killPoints;
		this.velocity_max = enemyData.velocity_max;
		this.velocityDecrease = enemyData.velocity_decrease;
		this.collisionDamage = enemyData.collisionDamage;
		this.animator.runtimeAnimatorController = enemyData.animatorController;
	}

	protected override void MoveUpdate () {
		float topSpritePoint = this.transform.position.y + spriteExtends.y;
		if (this.isEnabled && MainCamera.instance.IsPositionOutsideBottomOfCamera(topSpritePoint)) {
			StopAllCoroutines();
			ResetRenderable();
		}
	}

	public virtual void StartMoveRoutine() {
		StartCoroutine ( MoveRoutine() );
	}

	protected virtual IEnumerator MoveRoutine() {
		while (true) {
			MoveBackward();
			yield return null;
		}
		yield return null;
	}

	protected override void Collision(Collidable c) {
		switch (c.collidableType) {
		case CollidableType.player:
			c.GetComponent<Player_controls>().SubstractHealthPoints(this.collisionDamage);
			break;

		}
	}
}
